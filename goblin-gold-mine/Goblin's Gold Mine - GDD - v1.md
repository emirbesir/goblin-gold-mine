# **Goblin's Gold Mine GDD**

## **1\. Game Summary (Oyun Özeti)**

* **Başlık:** Goblin's Gold Mine  
* **Açıklama:** Açgözlü bir patron goblin olarak bir madeni yönettiğin, uyuklayan tembel işçileri dürterek çalıştırdığın ve altınlarına altın katarak maden imparatorluğunu genişlettiğin bir arcade-idle oyunu.  
* **Hedef Platform:** iOS / Android  
* **Hedef Kitle ve Tür:** Geniş Kitle, Hybrid-Casual (Arcade Idle \+ Time Management)  
* **Referans Oyunlar:**  
  * *My Little Universe / Dreamdale:* Kaynak toplayarak kapalı harita parçalarını açma mekaniği.  
  * *Eatventure / Burger Please\!:* Yapay zeka işçilerle üretim zincirini (otomasyon) büyütme ve yönetme.

## **2\. Core Mechanic (Temel Mekanikler)**

* **Ana Oynanış Döngüsü (Core Loop):**  
  1. **Goal:**  
     * Yeterli altını toplayarak hedeflenen yeni bölgenin kilidini açmak.  
     * İşçilerin hepsinin uyumasıyla üretimin ve kazancın durmasından kaçınmak.  
  2. **Act:**   
     * Joystick ile gezinerek madenleri kır ve altın topla.  
     * Belli aralıklarla uyuyakalan işçilerin yanına gidip onları uyandırarak üretime devam etmelerini sağlamak.  
  3. **Feedback:**  
     * Patron olarak işçilerin yanına gidip onları uyandırırken alınan haptic feedback (titreşim)  
  4. **Reward:**   
     * Kırılan madenlerden ve çalıştırılan işçilerden toplanan altınlar.  
  5. **Invest:**   
     * Toplanan altınlarla yeni "Tembel Goblin İşçiler" satın almak  
     * Karakterin taşıma kapasitesini ve hızını artırmak.  
     * Biriken devasa kaynakla haritanın kilitli, yeni ve daha değerli maden katmanlarını açmak.  
  6. **Hook:**  
     * Altın maliyetinin katlanarak artmasıyla oyuncunun otomasyon kurmaya (işçi almaya, onları hızlandırmaya ve uyanık tutmaya) zorlanması.  
     * Altınlarına altın katarak bir maden imparatorluğunu genişletme hissi.  
* **Kontrol Şeması:** Tek parmakla sanal joystick (Swerve/Navigasyon). UI butonları sadece "Upgrade" istasyonlarında çıkar.  
* **Fail / Win Koşulları:**   
  * *Fail Koşulu:* Klasik bir "Game Over" yok. Soft-fail durumu var, bu da işçilerin hepsinin uyuması ve üretimin/kazancın durmasıdır.  
  * *Win Koşulu:* Yeterli altını toplayarak hedeflenen yeni bölgenin kilidini açmak.  
* **Zorluk Eğrisi Taslağı:** İlk 2 bölgeyi oyuncu tek başına saniyeler içinde açabilir. 3\. bölgeden itibaren altın maliyeti katlanarak artar ve oyuncuyu işçi almaya, onları hızlandırmaya ve uyanık tutmaya (otomasyona) zorlar.

## **3\. Production Plan (Üretim Planı)**

* **Teknik Stack:**  
  * Motor: Unity 3D URP (C\#)  
* **Asset Listesi:**  
  * **3D:**   
    * Low-poly ana karakter (Patron Goblin),   
    * Low-poly işçi goblinler,   
    * Low-poly kayalar, maden cevherleri (altın, elmas, yakut),   
    * Depo (sandık)  
    * …  
  * **Ses (ASMR odaklı):**   
    * Tatmin edici kazma vuruş sesleri (pitch değeri rastgele değişen),   
    * Para şıngırtısı,   
    * İşçilerin uyuma (esneme/horlama) ve uyanma sesleri.  
* **Milestone Takvimi (StartGate 5 Haftalık Programı):**  
  * **1\. Hafta:** Mobil Oyun Tasarımı ve GDD dokümanının hazırlanması (Tamamlandı).  
  * **2\. Hafta:** Temel mekaniklere sahip oynanabilir bir konsept. (Unity'de joystick karakter kontrolü, kaynak kırma ve objeleri toplama loop'unun kodlanması).  
  * **3\. Hafta:** Oynanabilir bir prototip / Polishing. (AI işçilerin oyuna dahil edilmesi, uyuma/uyandırma mekaniği, DOTween ile görsel cilalama).  
  * **4\. Hafta:** Oynanabilir bir oyun. (Level tasarımlarının uzatılması, Reklam/SDK entegrasyonları ve erken aşama ekonomi ayarları).  
  * **5\. Hafta:** Demo Day. (Pitch Deck sunum hazırlığı ve oyunun test ettirilerek jüri geri bildirimlerinin alınması).  
* **Ekip Rolleri ve Sorumluluklar:**  
  * **Emir Beşir:** Software Engineer  
  * ***Beyzanur Akay:** 3D Artist*  
  * ***Yılmaz Emir Tunç:** Software Engineer*