# ChessTaskEFSignalR

Тестовое задание «Ход конем»: 
Требования: 
MVC, BL, DAL, PostgreSQL, EF, Bootstrap, SignalR
Реализацию осуществлять .net core

Введение:
Шахматная доска используется во множестве математических задач. Классическими являются задачи о перемещении различных фигур (пешки, слона, ладьи, ферзя) по шахматной доске. Особый интерес представляет следующий вопрос: может ли шахматный конь пройти по всем 64 клеткам доски ровно один раз и вернуться в исходную клетку?
Эта задача имеет решение; более того, оно не является единственным. Эту головоломку, как и многие другие шахматные задачи, можно решить с помощью теории графов. Каждая клетка доски соответствует вершине графа, ход коня — ребру, соединяющему две вершины графа. Следовательно, задача сводится к нахождению гамильтонова цикла в этом графе.

Задание. 
1.	Отобразить на страничке браузера игровое поле – шахматную доску. Добавить кнопку «Сброс» - по нажатию на которую осуществляется рестарт игры. Добавить кнопки «Пауза/Старт», которые останавливают и продолжают выполнение программы соответственно
2.	Фигурка коня ходит автоматически, с настраиваемой (0.5,1,2 секунды) периодичностью
3.	Расчет производится на стороне сервера
4.	Ходы коня записываются в лог, событие о ходе доставляется в браузер push уведомлением.
-----------------------------------

В данном проекте представлено решение этой задачи.