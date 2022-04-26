# Experiment
.NET 6.0 Frameworku hedeflenerek geliştirilmiştir.

## Test Coverage Raporu
Gereken araçlar:

___dotnet test___ : Ayrıca yükleme gerektirmez. .NET SDK ile beraber gelmektedir.

___reportgenerator___ Yüklemek için:	        
```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```
---
#### Test Coverage Raporunu Oluşturmak
Proje anadizininde (.sln dosyasının bulunduğu dizinde) aşağıdaki komut yürütülür:
```bash
dotnet test --collect:"XPlat Code Coverage"
```

Bu komut sonucunda **Experiment.Tests** projesi altında **TestResults** klasörü ve bu klasörün altında söz konusu yürütmenin sonucunu barındıran (GUID olarak adlandırılmış) bir başka klasör oluşacaktır. Her bir yürütme yeni bir rapor dolayısıyla bu raporu barındıracak TestResults altında yeni bir klasör oluşturacaktır.

## Extra: Coverage Raporunu Okunabilir Hale Getirmek
Coverage raporunu **reportgenerator** aracını kullanarak HTML'e çevirmek suretiyle daha kolay okunabilir hale getirebiliriz. 

HTML'e çevrilmek istenen rapor klasörünün ana dizinine geçilip aşağıdaki komut yürütülür:

```bash
reportgenerator "-reports:coverage.cobertura.xml" "-targetdir:./report"
```

Üstteki komutun yürütüldüğü dizinde oluşacak olan ___"report"___ klasöründeki ___index.html___ dosyası coverage raporunun anasayfasına denk gelmektedir.

---

Örnek olarak kodun son haline karşı yürütülmüş testlerin coverage raporunun HTML'e çevrilmiş hali repository'ye dahil edilmiştir:

**/Experiment.Tests/TestResults/f187c047-b34a-4286-b212-8ec588bfffb1/report/index.html**


## Request Örnekleri
Discount değerini döndürecek olan endpoint, GUID tipinde invoiceId alacak şekilde tasarlanmıştır.

Hangi GUID için hangi sonucun dönüleceği FakeUserRepository ve FakeInvoiceRepository incelenerek görülebilir.

Aşağıda değişik tipteki kullanıcılar için örnek request şekilleri belirtilmiştir:

Tipi **Affiliate** olan bir **User** için:
```bash
curl -k -X 'GET' 'https://localhost:7198/bill/discount/0246a66d-0627-4f11-aadd-2f91a725cf76' -H 'accept: */*'
```
Tipi **Affiliate** olan fakat bütün ürünleri "Grocery" kategorisinde olan bir **User** için:
```bash
curl -k -X 'GET' 'https://localhost:7198/bill/discount/6b0b2241-cca8-478e-9f3c-c70c4444d46a' -H 'accept: */*'
```
Tipi **Customer** ve 2 yıldan fazla bir süredir üye olan bir **User** için:
```bash
curl -k -X 'GET' 'https://localhost:7198/bill/discount/0a331800-25e1-49f4-914a-11869a383ead' -H 'accept: */*'
```
Tipi **Customer** ve henüz üye olmuş bir **User** için:
```bash
curl -k -X 'GET' 'https://localhost:7198/bill/discount/2d4b06f2-c380-4681-8ba0-29b42433513b' -H 'accept: */*'
```
Tipi **Employee** olan bir **User** için:
```bash
curl -k -X 'GET' 'https://localhost:7198/bill/discount/473cb273-5142-4ccd-9e40-8e6bb1eaf75b' -H 'accept: */*'
```


Daha okunaklı bir JSON response çıktısı için:
```bash
json_pp -json_opt pretty,canonical
```
ifadesiyle piping yapılabilir:
```bash
curl -k -X 'GET' 'https://localhost:7198/bill/discount/0246a66d-0627-4f11-aadd-2f91a725cf76' -H 'accept: */*' | json_pp -json_opt pretty,canonical
```
