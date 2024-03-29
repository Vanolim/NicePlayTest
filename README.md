# NicePlayTest
 
![hippo](https://media.giphy.com/media/u3RJ72744beMmLp5FN/giphy.gif)

# Особенности проекта:
Тестовое - https://drive.google.com/file/d/1vRj0t8SJFKy1BWuXUdhUWksmmjo6hBLe/view?usp=drive_link
1) Из-за неплохой инкапсуляции и SRP было создано значительное количество сущностей
для такого проекта. Для облегчения разработки и лучше читаемости кода был
использован Zenject
2) Маскимальное количество ингредиентов для блюдо - изменяемое число. Все
необходимые данные и view подтягиваются автоматически
3) Все возможные комбинации сохранются в файл AllDishCombination.txt. Если такого
файла нет, он будет создан автоматически. Файл находится по адресу
Application.persistentDataPath + «/AllDishCombination.txt»
4) Информация о ценности объекта берется из файла IngredientValue.xml, который
находится по адресу Application.persistentDataPath + «/IngredientValue.xml». Однако если
такого файла не будет, он будет создан автоматически. Данные ценности будут браться
из SO каждого ингредиента
5) В Update с определенным delay (для оптимизации) происходит поиск интерактивных
элементов по позиции курсора. Причем игра сначала пытается найти объект обычным
лучем, если это не удается - игра пытается найти интерактивный объект окружности.
6) Все эти параметры задаются в скрипте «InputInteractDetector»
7) Анимация тления углей сделана изменение alpha у спрайта горящих углей. Значения
alpha берется из анимационной кривой текущего пресета. Текущий пресет
определяется тем есть ли ингредиенты в котле или нет. Изменения пресетов
происходит плавно за определенное время
8) Добавление ингредиентов происходит легко. Достаточно создать новый тип
ингредиента, создать SO с информацией о новом ингредиенте и создать spot на сцене,
который будет спавнить нужный ингредиент
9) Информация о блюде учитывает не только количество объектов, которые должны быть
в блюде, но также и отсутствующие ингредиенты. Т.е. можно сделать блюдо из 3
картошек, но также блюдо можно сделать если в нем не будет 2 мясо
10) Проект разделен на asmdef по смыслу
11) Я не привык писать комментарии к коду, потому что меня учили, что комментарии -
плохой тон и код должен быть таким, чтобы его можно было спокойно читать без
комментариев. Все же постарался добавить комментарии в основные места
12) Я сделал сохранение и загрузку данных, но я не понял момент про кнопку L. Так как
данные подгружается автоматически (если они есть) при старте игры. При нажатии
кнопки restart данные сбрасываются и будут перезаписаны сразу же после создания
нового блюда. Я не совсем понял зачем и в каком моменты нужно принудительно
подгружать данные на кнопку L
13) Для выделения интерактивных объектов при ховере использовался плагин 2D Sprite
Outline. Я решил не писать шейдер самому, потому что это заняло бы еще больше
времени на разработку

# Видео-демонстрация
https://youtu.be/XJkj_c5W8Ok

# Билд пк
https://drive.google.com/file/d/1O67r7OauJfc7sXsuDmGFenSgRkDcKmHP/view?usp=sharing

# Package
https://drive.google.com/file/d/1fxvoL5QH_3PGRTFETchZMnGu2l8u-ez5/view?usp=sharing
