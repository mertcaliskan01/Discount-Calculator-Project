# DiscountCalculatorAPI

DiscountCalculatorAPI, .NET 8 ile geliştirilmiş basit ve genişletilebilir bir e-ticaret indirim hesaplama API'sidir.  
Kategori ve müşteri tipine göre indirim oranlarını hesaplar, sonucu JSON olarak sunar.  
Swagger/OpenAPI desteği ile kolayca test edilebilir ve genişletilmeye uygundur.

## Özellikler

- **Kategori bazlı indirim**
- **Müşteri tipi bazlı ek indirim**
- **Toplu alımda ekstra indirim**
- **Temel validasyon ve hata yönetimi**
- **Swagger UI ile interaktif dökümantasyon**
- **Unit test desteği (dotnet test)**

## Kurulum ve Çalıştırma

### 1. Depoyu Klonla

```sh
git clone https://github.com/mertcaliskan01/Discount-Calculator-Project.git
cd Discount-Calculator-Project
```

### 2. Bağımlılıkları Yükle

Ana dizinde:
```sh
dotnet restore
```

### 3. API'yi Başlat

`src/DiscountCalculatorAPI` dizinine girip:
```sh
dotnet run
```

API aşağıdaki adreste çalışacaktır:
```
http://localhost:5080
```

### 4. Swagger (OpenAPI) Arayüzü

API dokümantasyonu ve endpoint testleri için:
```
http://localhost:5080/swagger/index.html
```

### 5. Testleri Çalıştır

Ana dizinde:
```sh
dotnet test
```

Tüm testler otomatik olarak çalışacaktır.

## API Kullanımı

### İndirim Hesaplama (POST /api/product/calculatediscount)

#### İstek Örneği

```json
{
  "productName": "Akıllı Telefon",
  "category": "Elektronik",
  "originalPrice": 5000,
  "quantity": 2,
  "customerType": "VIP"
}
```

#### Yanıt Örneği

```json
{
  "productName": "Akıllı Telefon",
  "originalPrice": 5000,
  "categoryDiscount": 750,
  "customerDiscount": 250,
  "quantityDiscount": 0,
  "totalDiscount": 1000,
  "finalPrice": 4000
}
```

## İndirim Kuralları

- **Kategori İndirimi**
  - Elektronik: %15
  - Giyim: %20
  - Ev & Yaşam: %10
- **Müşteri Tipi**
  - VIP müşteri: %5 ek indirim
- **Toplu Alım**
  - 5 ve üzeri adet: %10 ek indirim

## Proje Yapısı

```
Discount-Calculator-Project/
│
├── src/
│   └── DiscountCalculatorAPI/      # API uygulaması
│
├── tests/
│   └── DiscountCalculatorAPI.Tests/   # Unit testler
│
└── README.md
```

---

**Geliştirici:**  
[Mert Çalışkan](https://github.com/mertcaliskan01)
