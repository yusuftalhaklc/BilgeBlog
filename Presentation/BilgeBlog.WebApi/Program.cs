using AspNetCoreRateLimit;
using BilgeBlog.Application.DependencyResolvers;
using BilgeBlog.Application.Validators.PostValidators;
using BilgeBlog.Persistence.DependencyResolvers;
using BilgeBlog.WebApi.DependencyResolvers;
using BilgeBlog.WebApi.Middleware;
using BilgeBlog.WebApi.Validators.PostValidators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BilgeBlog.Service.DependencyResolver;

namespace BilgeBlog.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContextService();
            builder.Services.AddRepositoryService();
            builder.Services.AddAutoMapperService();
            builder.Services.AddWebApiAutoMapperService();
            builder.Services.AddMediatRService();

            // CORS
            var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", policy =>
                {
                    policy.WithOrigins(corsOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // Health Checks
            builder.Services.AddHealthChecks();

            // Rate Limiting
            builder.Services.AddMemoryCache();
            builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
            builder.Services.AddInMemoryRateLimiting();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            // FluentValidation
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssembly(typeof(CreatePostCommandValidator).Assembly);
            builder.Services.AddValidatorsFromAssembly(typeof(CreatePostRequestValidator).Assembly);

            builder.Services.AddInfrastructureServices();
            builder.Services.AddJwtTokenService();
            builder.Services.AddAuthorization();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "BilgeBlog API",
                    Version = "v1",
                    Description = @"# BilgeBlog API Kullanım Kılavuzu

## Genel Bilgiler
BilgeBlog API, blog yazıları, kategoriler, yorumlar ve beğeniler için RESTful bir API sağlar.

## Kimlik Doğrulama (Authentication)

### 1. Kullanıcı Kaydı
- **Endpoint:** `POST /api/User/register`
- **Açıklama:** Yeni bir kullanıcı hesabı oluşturur
- **Gerekli Alanlar:**
  - `FirstName`: Kullanıcı adı
  - `LastName`: Kullanıcı soyadı
  - `Email`: E-posta adresi
  - `Password`: Şifre (minimum 6 karakter)

### 2. Giriş Yapma
- **Endpoint:** `POST /api/User/login`
- **Açıklama:** Kullanıcı girişi yapar ve JWT token alır
- **Gerekli Alanlar:**
  - `Email`: E-posta adresi
  - `Password`: Şifre
- **Yanıt:** `Token` ve `RefreshToken` döner

### 3. Token Kullanımı
Swagger'da sağ üstteki **Authorize** butonuna tıklayın ve şu formatta token girin:
```
Bearer YOUR_JWT_TOKEN
```

### 4. Token Yenileme
- **Endpoint:** `POST /api/User/refresh-token`
- **Açıklama:** Süresi dolmuş token'ı yeniler
- **Gerekli Alanlar:**
  - `Token`: Mevcut JWT token
  - `RefreshToken`: Refresh token

## Roller ve Yetkiler

### Admin
- Kategori oluşturma, güncelleme, silme
- Post oluşturma, güncelleme, silme

### Author
- Post oluşturma, güncelleme, silme

### User
- Post görüntüleme
- Yorum yapma
- Beğeni yapma

## API Endpoint'leri

### Kategoriler (Categories)
- `GET /api/Category` - Tüm kategorileri listele (Herkes)
- `GET /api/Category/{id}` - Kategori detayı (Herkes)
- `POST /api/Category` - Kategori oluştur (Sadece Admin)
- `PUT /api/Category/{id}` - Kategori güncelle (Sadece Admin)
- `DELETE /api/Category/{id}` - Kategori sil (Sadece Admin)

### Postlar (Posts)
- `GET /api/Post` - Tüm postları listele (Herkes)
  - Query Parametreleri: `pageNumber`, `pageSize`, `search`, `tagName`, `categoryId`
- `GET /api/Post/{id}` - Post detayı (Herkes)
- `POST /api/Post` - Post oluştur (Admin veya Author)
  - **Önemli:** Post oluştururken tam olarak **5 tag** girilmelidir
  - Her tag minimum **2** maksimum **50 karakter** olmalıdır
- `PUT /api/Post/{id}` - Post güncelle (Admin veya Author)
- `DELETE /api/Post/{id}` - Post sil (Admin veya Author)

### Yorumlar (Comments)
- `GET /api/Post/{postId}/comment` - Post yorumlarını listele (Herkes)
  - Query Parametreleri: `pageNumber`, `pageSize`
- `POST /api/post/{postId}/comment` - Yorum ekle (Giriş yapmış kullanıcı)
- `POST /api/post/{postId}/comment/{commentId}/reply` - Yorum yanıtla (Giriş yapmış kullanıcı)

### Beğeniler (Likes)
- `GET /api/Post/{postId}/like` - Post beğenilerini listele (Herkes)
  - Query Parametreleri: `pageNumber`, `pageSize`
- `POST /api/post/{postId}/like` - Post beğen (Giriş yapmış kullanıcı)
- `DELETE /api/post/{postId}/like` - Beğeniyi kaldır (Giriş yapmış kullanıcı)

## Sayfalama (Pagination)
Listeleme endpoint'lerinde sayfalama için:
- `pageNumber`: Sayfa numarası (varsayılan: 1)
- `pageSize`: Sayfa başına kayıt sayısı (varsayılan: 10)

## Arama ve Filtreleme

### Post Arama
- `search`: Başlık veya içerikte arama (case-insensitive)
- `tagName`: Tag adına göre filtreleme
- `categoryId`: Kategori ID'sine göre filtreleme

### Kategori Arama
- `search`: Kategori adında arama (case-insensitive)

## Hata Yönetimi
API, standart HTTP durum kodlarını kullanır:
- `200 OK`: İşlem başarılı
- `400 Bad Request`: Geçersiz istek
- `401 Unauthorized`: Kimlik doğrulama gerekli
- `403 Forbidden`: Yetki yetersiz
- `404 Not Found`: Kayıt bulunamadı
- `429 Too Many Requests`: Rate limit aşıldı

## Rate Limiting
- Genel limit: Dakikada 100 istek
- Login: Dakikada 5 istek
- Register: Dakikada 3 istek

## Health Check
- `GET /health` - Uygulama sağlık durumunu kontrol eder

## Örnek Kullanım Akışı

1. **Kayıt Ol:** `POST /api/User/register` ile yeni hesap oluştur
2. **Giriş Yap:** `POST /api/User/login` ile token al
3. **Token'ı Ayarla:** Swagger'da Authorize butonuna tıkla ve token'ı gir
4. **Post Oluştur:** `POST /api/Post` ile yeni post oluştur (5 tag ile)
5. **Postları Listele:** `GET /api/Post` ile tüm postları görüntüle
6. **Yorum Yap:** `POST /api/post/{postId}/comment` ile yorum ekle
7. **Beğen:** `POST /api/post/{postId}/like` ile postu beğen"
                });

                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // Swagger her ortamda açık (Development ve Production)
            app.UseSwagger();
            app.UseSwaggerUI();

            // Rate Limiting
            app.UseIpRateLimiting();

            app.UseHttpsRedirection();

            // CORS
            app.UseCors("AllowSpecificOrigins");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            // Health Checks
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = async (context, report) =>
                {
                    context.Response.ContentType = "application/json";
                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        status = report.Status.ToString(),
                        checks = report.Entries.Select(x => new
                        {
                            name = x.Key,
                            status = x.Value.Status.ToString(),
                            exception = x.Value.Exception?.Message,
                            duration = x.Value.Duration.ToString()
                        })
                    });
                    await context.Response.WriteAsync(result);
                }
            });

            app.MapControllers();

            app.Run();
        }
    }
}
