# Feline-Rampage
## 

Проект выполнен Калашниковым Егором и Ибрагимовой Алиной.
Курсовая работа находится в разработке, это сырая, но рабочая версия 
проекта.

## Описание:

"Feline Rampage: The Hunger Chronicles" - это захватывающая компьютерная Roguelike игра в ретро стиле, созданная на движке Unity. Вам предстоит взять на себя роль голодного уличного кота, чья единственная цель - найти пищу. Путешествуйте по опасным уголкам Улицы, открывая новые уровни, и сражайтесь с различными противниками. Используйте свои инстинкты, ловкость и тактику, чтобы победить врагов и найти еду, которая сделает Вашего персонажа сильнее и наделит его особыми способностями. Все это представлено в классическом ретро стиле и от первого лица, чтобы погрузить вас в атмосферу старых игр. Готовы ли вы справиться с вызовом и помочь голодному коту утолить свой голод?

## Что готово:
### 1) Юнит(враждебный моб)
1. Класс юнита реализованы  MVC паттерном.
2. Рандомная генерация от 1 до 3 юнитов в комнате.

### 2) Предметы:
1. Классы предмета реализованы MVC паттерном, есть два типа предметов - `Artifact` и `Consumable`.
2. Рандомная генерация предметов, от 1 до 3 и разные типы генераций с разными предметами по редкости.

### 3) Карта(комнаты):
1) У каждой комнаты свой префаб с параметрами настраиваемыми в Unity.
2) Перемещение по комнатам осуществляется при помощи игрового интерфейса.

### 4) Игровой интерфейс:
1) У камеры есть интерфейс, в который входят кнопки для передвижения игрока.
2) Кол-во монет игрока.
### 5) Боёвка:
1. Один тип врагов: крыса. Удары крысы: удар мимо, дефолтный удар и 
критический удар (рассчитывается от параметра удачи врага). 
2. Сегметированная боёвка для главного игрока: выбор, куда бить 
(голова, конечности, тело), в зависимости от чего рассчитывается 
сила и вероятность удара(пропуск удара, дефолтная атака, критическая)
и статистики игрока/врага после удара (хп, сила, ловкость, удача).
3. Бой со стороны игрока выглядит как бой по клику - продолжение боя
зависит от наличия клика.
3. Уворот: и у моба, и у гг - зависит от ловкости. 
4. Рандомная генерация врагов в стартовой комнате.
5. Механизм перехода в файт-мод с врагом с помощью коллайдеров.
6. Вся информация о ходе боя представлена в логах программы на консоли.
### 6) Графика
1. Большинство задников готовы.
2. Большинство иконок для предметов готовы.
3. Несколько спрайтов для юнитов готовы.
4. Спрайты/иконки для интерфейса почти готовы.
## to do:
1. Разные враги и боссы.
2. Генерация мобов и предметов в разных комнатах, не только в стартовой.
3. Монеты и операции с ними: выпадение монет с врагов + возможность 
купить что-то на них в магазине.
4. Реализация магазина, где можно купить предметы для баффа статистик
главного игрока.
5. Реализация генерации рандомной карты для трёх уровней (как в 
играх-рогаликах).
6. Реализация инвентаря.
7. Возможность сохранения игры.
8. Инвентарь для всех собранных предметов + его отображение в 
пользовательском интерфейсе.
9. Хелф-бары у гг и врагов.
10. Отображение игровых характеристик игрока.
11. Логи боя на пользовательский интерфейс.
