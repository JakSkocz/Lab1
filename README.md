# Program symulujący czujnik temperatury

### 1. Zawiera metodę generującą pojedynczą wartość z przedziału [-100, 100] (class Random). Wartość zwracana przez metodę jest typu double? (typu Nullable). Wartości poniżej -80 interpretowane są jako błędny odczyt, w takim przypadku metoda zwracawartość null.

### 2. Zawiera metodę generującą N wartości zmiennoprzecinkowych. Wylosowane liczby są zapisywane do listy (List<double?>)

### 3. Zawiera funkcjonalność wyświetlania listy na ekranie. Wartości są wyświetlane z dokładnością do dwóch miejsc po przecinku.

### 4. Zawiera funkcjonalność zapisywania wartości z listy do pliku z wykorzystaniem mechanizmu serializacji, nazwa pliku bazuje na czasie jego utworzenia.

### 5. Zawiera metodę deserializującą plik zawierający wartości losowe.

### 6. Zawiera funkcjonalność usuwania pliku z dysku twardego po wczytaniu jego zawartości.

### 7. Zawiera interfejs (ISensor) udostępniający funkcjonalności czujnika.

### 8. Zawiera zaimplementowaną klasę (Program) która wykorzystuje interfejs ISensor, której zadaniem jest obliczanie wartości średniej, odchylenia standardowego, sortowanie listy wartości z możliwością zapisania wyniku do pliku, usuwanie wybranych zakresów wartości z możliwością zapisania wyniku do pliku.
