data = {}
def create():
    name = input("Введите название заметки: ").strip()
    note = input("Напишите заметку, которую вы хотите добавить: ").strip()
    data[name] = "Текст: " + note


def delete():
    name = input("Введите название заметки: ").strip()
    if name in data:
        del data[name]
        print("Заметка успешно удаленна")
    else:
        print("Проверьте корректность названия заметки и введите еще раз")

def search():
    name = input("Введите название заметки: ").strip()
    if name in data:
        print(data[name])
    else:
        print("Проверьте корректность названия заметки и введите еще раз")
def close():
    print("Вы закрыли заметку ")
    exit()
def show():
    name = input("Введите название заметки: ").strip()
def start_manager():
    with open('Bd.txt', 'r', encoding="UTF-8") as f:
        txt = f.read()
        splited_txt = txt.split("_")
        for i in splited_txt:
            i = i.strip().split("\n")
            print(i)
            data[i[0][10:]] = "\n".join(i[1:])

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
        print(data)
start_manager()
interface()

