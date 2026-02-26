# 🚇 3D Horror Project - Portfolio Showcase

**3D Horror Project**, bir metro güvenlik görevlisinin gözünden deneyimlenen, atmosferik bir korku oyununun teknik dikey kesitidir (vertical slice). Bu proje bir ekip çalışması olarak başlamış olup, **harita tasarımları hariç** tüm teknik mimari, mekanik sistemler, UI/UX ve görsel shader çalışmaları tarafımdan geliştirilmiştir.

---

## 🎨 Sanatsal & Teknik Görsel Çalışmalar (Technical Art)

Oyunun atmosferini güçlendirmek için standart Unity materyalleri yerine kendi yazdığım özel shader çözümlerini uyguladım.

### 🎥 Yarı İnteraktif Ana Menü (Metro Exodus Style)
* **Daldırma (Immersion):** Oyuncuyu henüz menüdeyken bile oyunun içinde hissettirmek istedim. Güvenlik odasında oturan karakterimiz ve monitörler üzerinden yönetilen bir UI tasarladım.
* **Teknik:** Kamera geçişleri (Cinemachine) ve dünya uzayında (World Space Canvas) render edilen arayüz elementlerinin optimizasyonu sağlandı.

![İnteraktif Ana Menü Deneyimi](

https://github.com/user-attachments/assets/520233e6-f204-418b-8cf5-f67744f84704

)

### 👾 PSX Style & Pixel Shader
* **Nostaljik Korku Atmosferi:** Modern grafikleri bilinçli olarak düşük çözünürlüklü ve "jittery" (titrek/vertex snapping) bir hale getiren bir post-processing ve materyal shader'ı yazdım. 
* **Tasarım Amacı:** Bu yöntem hem retro korku oyunlarının (eski PS1 oyunları) o tekinsiz hissini veriyor hem de düşük poligonlu modellerin estetik bir bütünlük içinde görünmesini sağlıyor.

![PSX Shader Efekti](

https://github.com/user-attachments/assets/21802a8d-e829-4aed-b0b8-28a8b5151743

)

### 👻 Yarı Transparan Düşman Shader'ı (Semi-Transparent Silhouette)
* **Siluet Etkisi:** Düşmanın tüm detaylarıyla görünmesi yerine, korku unsurunu artırmak adına sadece bir gölge/siluet gibi algılanması için yarı transparan, derinlik ve form algısı olan özel bir shader geliştirdim.

![Düşman Shader Siluet Etkisi](

https://github.com/user-attachments/assets/7bf109a8-626c-4b1b-a44c-b8d3448a886b

)

---

## 🛠 Teknik Mimari ve Sistemler

Bu projede temel odak noktam, temiz kod prensiplerine uygun, modüler ve performansı yormayan sistemler kurmaktı.

### 🎒 Envanter ve Etkileşim Sistemi (Inventory & Interaction)
* **ScriptableObject Tabanlı Mimari:** Eşya verilerini yönetmek için ScriptableObject yapısını kullandım. Bu sayede yeni bir eşya eklemek veya dengelemek, koda dokunmadan doğrudan editör üzerinden yapılabiliyor.
* **Eşya İnceleme (Inspect System):** Oyuncunun eşyayı 3D uzayda detaylıca incelemesini sağlayan bir sistem kurguladım. İnceleme sırasında oyun akışını yönetmek için durum tabanlı (State-based) bir yaklaşım izledim.
* **Fırlatma ve Toplama:** Fizik (Rigidbody) tabanlı fırlatma mekaniği ile `Raycast` tabanlı hassas obje toplama sistemini entegre ettim.

![Envanter ve İnceleme Sistemi](

https://github.com/user-attachments/assets/231c0328-30cf-4d06-a0b3-7faa7a694788

)

### 🗺 Görev Sistemi (Quest System)
* Durum makinesi (**State Machine**) mantığıyla çalışan, oyuncunun mevcut ilerlemesini takip eden ve kullanıcı arayüzü (UI) ile entegre bir merkezi görev yöneticisi tasarladım.

---

## ⚙️ Kullanılan Teknolojiler
* **Game Engine:** Unity 3D
* **Programming Language:** C#
* **Graphics/Rendering:** Shader Graph / HLSL (Custom Shaders)
* **Version Control:** Git & GitHub

---

## 👨‍💻 Geliştirici Notu (Post-Mortem)
Bu proje benim Unity üzerindeki teknik yetkinliklerimi (C# sistem mimarisi ve optimizasyon) ve sanatsal vizyonumu (Technical Art & Shader & UI/UX) birleştirdiğim bir laboratuvar oldu. Proje tam bir oyun olarak yayınlanmasa da, kapsam yönetimi (scope management) ve sıfırdan ölçeklenebilir sistemler kurma konularında bana çok büyük mühendislik tecrübeleri kazandırdı.
