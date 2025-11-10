# ⚽ FootballAdmin

A comprehensive football management system built with ASP.NET Core 8. This application allows administrators and managers to manage footballers, teams, matches, formations, and training sessions with real-time data integration from EA Sports FC25.

**Kapsamlı Futbol Yönetim Sistemi** - ASP.NET Core 8 ile geliştirilmiş bu uygulama, yöneticilerin ve menajerlerin futbolcu, takım, maç, formasyon ve antrenman yönetimini EA Sports FC25'ten gerçek zamanlı veri entegrasyonu ile yapmasına olanak tanır.

---

## 🚀 Features / Özellikler

### Admin Features / Yönetici Özellikleri
- **Player Management** / **Oyuncu Yönetimi**: Add, edit, and manage players with detailed statistics
- **Team Management** / **Takım Yönetimi**: Create and manage teams
- **Manager Management** / **Menajer Yönetimi**: Manage manager accounts and permissions
- **Match Management** / **Maç Yönetimi**: Schedule and track matches
- **Training Session Management** / **Antrenman Yönetimi**: Organize training sessions
- **Notification System** / **Bildirim Sistemi**: Send notifications to managers
- **Dashboard** / **Kontrol Paneli**: View comprehensive statistics and overview

### Manager Features / Menajer Özellikleri
- **Club Management** / **Kulüp Yönetimi**: Manage your club, players, and budget
- **Formation Management** / **Formasyon Yönetimi**: Create and manage team formations (4-4-2, 4-3-3, 3-5-2, etc.)
- **Match Proposals** / **Maç Teklifleri**: Propose and accept match requests
- **Transfer Market** / **Transfer Pazarı**: Buy and sell players
- **Player Statistics** / **Oyuncu İstatistikleri**: View detailed player statistics and charts
- **Rival Management** / **Rakip Yönetimi**: Manage rival teams
- **Profile Management** / **Profil Yönetimi**: Update personal information and settings

### Data Integration / Veri Entegrasyonu
- **EA Sports FC25 Integration** / **EA Sports FC25 Entegrasyonu**: Automatically fetch player attributes and statistics from EA Sports FC25 official website
- **Google Custom Search API** / **Google Özel Arama API**: Automatically fetch player images using Google Custom Search
- **Real-time Statistics** / **Gerçek Zamanlı İstatistikler**: Dynamic player statistics with charts and visualizations

---

## 🛠️ Technologies / Teknolojiler

- **.NET 8.0** - Latest .NET framework
- **ASP.NET Core MVC** - Web application framework
- **Entity Framework Core 8.0.10** - ORM for database operations
- **SQL Server** - Database management system
- **Cookie Authentication** - User authentication and authorization
- **Google Custom Search API** - Image search integration
- **SweetAlert2** - Beautiful alert dialogs
- **Bootstrap & Custom Theme** - Modern UI/UX

---

## 📁 Project Structure / Proje Yapısı

```
FootballAdmin/
├── Entities/           # Domain entities and models
├── DataAccess/         # Data access layer (Repository pattern)
├── Business/           # Business logic layer
├── Core/               # Core interfaces and generic repository
└── WebUI/              # Presentation layer (MVC)
    ├── Areas/
    │   ├── Admin/      # Admin area controllers and views
    │   └── Manager/    # Manager area controllers and views
    ├── Controllers/    # Main controllers
    ├── Views/          # Razor views
    └── wwwroot/        # Static files (CSS, JS, images)
```

### Architecture / Mimari
- **N-Layer Architecture** / **N-Katmanlı Mimari**: Separation of concerns with distinct layers
- **Repository Pattern** / **Repository Deseni**: Generic repository for data access
- **Dependency Injection** / **Bağımlılık Enjeksiyonu**: Built-in DI container
- **Area-based Routing** / **Alan Tabanlı Yönlendirme**: Separate areas for Admin and Manager

---

## 📋 Requirements / Gereksinimler

- **.NET 8.0 SDK** or later
- **SQL Server** (LocalDB, Express, or Full version)
- **Visual Studio 2022** or **Visual Studio Code** (recommended)
- **Google Custom Search API Key** (optional, for player images)
- **Google Custom Search Engine ID (CSE_ID)** (optional, for player images)

---

## 🚀 Installation / Kurulum

### 1. Clone the Repository / Depoyu Klonlayın

```bash
git clone <repository-url>
cd AFootballProjectASP.NET/FootballAdmin
```

### 2. Database Configuration / Veritabanı Yapılandırması

1. Open `WebUI/appsettings.json`
2. Update the `FootballAdminConnection` connection string:

```json
{
  "ConnectionStrings": {
    "FootballAdminConnection": "Server=YOUR_SERVER;Database=FootballAdmin;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
  }
}
```

**Türkçe:**
1. `WebUI/appsettings.json` dosyasını açın
2. `FootballAdminConnection` bağlantı dizesini güncelleyin

### 3. Database Migration / Veritabanı Migrasyonu

Run the following commands in the Package Manager Console or terminal:

```bash
# Navigate to DataAccess project
cd DataAccess

# Create migration
dotnet ef migrations add InitialCreate --startup-project ../WebUI

# Update database
dotnet ef database update --startup-project ../WebUI
```

**Alternatif:** If you have the SQL script, you can execute `sql_data_scripts.sql` directly in SQL Server Management Studio.

### 4. Google API Configuration (Optional) / Google API Yapılandırması (İsteğe Bağlı)

If you want to use player image search:

1. Get a Google Custom Search API Key from [Google Cloud Console](https://console.cloud.google.com/)
2. Create a Custom Search Engine and get the CSE_ID from [Google Custom Search](https://cse.google.com/)
3. Open `Business/Concrete/Image/ImageSearchManager.cs`
4. Update the API key and CSE_ID:

```csharp
private readonly string _apiKey = "YOUR_API_KEY";
private readonly string _cx = "YOUR_CSE_ID";
```

**Türkçe:**
1. [Google Cloud Console](https://console.cloud.google.com/) üzerinden bir API anahtarı alın
2. [Google Custom Search](https://cse.google.com/) üzerinden bir Özel Arama Motoru oluşturun ve CSE_ID alın
3. `Business/Concrete/Image/ImageSearchManager.cs` dosyasını açın
4. API anahtarı ve CSE_ID'yi güncelleyin

### 5. Run the Application / Uygulamayı Çalıştırın

```bash
cd WebUI
dotnet run
```

Or press `F5` in Visual Studio.

The application will be available at `https://localhost:5001` or `http://localhost:5000`

---

## 🔐 Default Users / Varsayılan Kullanıcılar

The application uses role-based authentication with two roles:
- **Admin** - Full system access
- **Manager** - Club and team management access

**Note:** Default user credentials should be set up in the database. Check the `sql_data_scripts.sql` file for sample data.

---

## 📸 Screenshots / Ekran Görüntüleri

### Add Player / Oyuncu Ekleme
![add_player](https://github.com/user-attachments/assets/81e01009-bda4-4209-b690-d2a00e09d3d7)

The system can automatically fetch player attributes from EA Sports FC25 official website.
Sistem, futbolcu bilgilerini doğrudan EA Sports FC25'in resmi sitesinden çekebilir.

### Profile Manager / Profil Yöneticisi
![profile_manager](https://github.com/user-attachments/assets/b37360a8-926b-45eb-98f5-b961fb95aca6)

### Match Management / Maç Yönetimi
![match](https://github.com/user-attachments/assets/8330638d-9717-428a-8239-38ba13ec8fcf)
![matches](https://github.com/user-attachments/assets/f14888db-d3d3-431e-a0ca-536813871eba)

### Player Card / Oyuncu Kartı
![bellingham_card](https://github.com/user-attachments/assets/811c6d61-ec13-4b22-8cec-ae0b5e00cfd1)

### Club Formation / Kulüp Formasyonu
![club_formation](https://github.com/user-attachments/assets/d9e3b387-d247-47e5-9b16-d247930fb2f2)

### Player Profile / Oyuncu Profili
![messi_profile](https://github.com/user-attachments/assets/8384dc73-37e4-4e64-af54-3b6ace032cd0)

### Player Statistics Chart / Oyuncu İstatistik Grafiği
![messi_chart](https://github.com/user-attachments/assets/8111cdce-9821-4c5d-a573-c8aba949fe65)

### Players List / Futbolcular Listesi
![futbolcular](https://github.com/user-attachments/assets/d74bf748-dd7d-46d7-9404-d88b650ab3ae)

---

## 🎯 Key Features Explained / Temel Özellikler Açıklaması

### EA Sports FC25 Integration / EA Sports FC25 Entegrasyonu
The system can automatically fetch player attributes, statistics, and data from the EA Sports FC25 official website. This includes:
- Player ratings and abilities
- Position information
- Detailed statistics (Pace, Shooting, Passing, Dribbling, Defending, Physicality)
- Base statistics

**Türkçe:** Sistem, EA Sports FC25 resmi web sitesinden otomatik olarak futbolcu özelliklerini, istatistiklerini ve verilerini çekebilir.

### Player Image Search / Oyuncu Resim Arama
Using Google Custom Search API, the system can automatically find and display player images when adding new players to the system.

**Türkçe:** Google Özel Arama API'sini kullanarak, sistem yeni oyuncular eklerken otomatik olarak oyuncu resimlerini bulup gösterebilir.

### Formation Management / Formasyon Yönetimi
Managers can create and manage team formations with drag-and-drop functionality. Supported formations include:
- 4-4-2
- 4-3-3
- 3-5-2
- 4-2-3-1
- 5-3-2
- And more...

**Türkçe:** Menajerler, sürükle-bırak işlevselliği ile takım formasyonları oluşturup yönetebilir.

---

## 🔧 Configuration / Yapılandırma

### Connection String / Bağlantı Dizesi
Update the connection string in `WebUI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "FootballAdminConnection": "Server=YOUR_SERVER;Database=FootballAdmin;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"
  }
}
```

### Authentication / Kimlik Doğrulama
The application uses Cookie Authentication with role-based authorization. Configure authentication settings in `Program.cs`:

```csharp
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });
```

---

## 📝 License / Lisans

This project is open source and available under the MIT License.

---

## 🤝 Contributing / Katkıda Bulunma

Contributions are welcome! Please feel free to submit a Pull Request.

Katkılarınız bekleniyor! Lütfen bir Pull Request göndermekten çekinmeyin.

---

## 📧 Contact / İletişim

For questions or support, please open an issue on GitHub.

Sorularınız veya destek için lütfen GitHub'da bir issue açın.

---

## 🙏 Acknowledgments / Teşekkürler

- EA Sports FC25 for player data
- Google Custom Search API for image search
- All contributors and testers

---

**Made with ❤️ using ASP.NET Core 8**
