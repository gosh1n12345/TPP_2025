def create():
    note = input("Напишите заметку, которую вы хотите добавить")

def delete():
    pass
def search():
    pass
def close():
    print("Вы закрыли заметку")
    exit()
def show():
    pass
def interface():
    print("Здравствуй, пользователь! Я менеджер твоих заметок")
    while True:
        print('''Вот список команд:
        1 - создать заметку
        2 - удалить заметку
        3 - искать заметку
        4 - закрыть заметку
        5 - показать заметку
        Введите номер выбранной команды''')
        ans = input()
        if ans == "1":
            create()
        elif ans == "2":
            delete()
        elif ans == "3":
            search()
        elif ans == "4":
            close()
        elif ans == "5":
            show()
        else:
            print("Вы ввели не то, что я просил. Введите цифру")
            continue
file = open("Bd.txt", "r+")

interface()

