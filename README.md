# VerstaTestTask

## ��� ������� � ��������� ����������:

1. ����������� �����������

```git clone https://github.com/sergeyfedorov02/VerstaTestTask.git```

2. ������� � ������� �������

```cd VerstaTestTask```

3. ������� ���������� ��� ������ web-�������

```dotnet publish -p:PublishProfile=FolderProfile```

4. ������� � ������� � ����������� ������ � ��������� ���

```
cd bin\Release\net9.0\publish
VerstaTestTask.exe
```

���������� ����� �������� �� ������ *http://localhost:5000/*

## ���������� � �������:
1. ��� ������������ ��������� ������� ������������� Radzen Blazor Studio

*https://www.radzen.com/blazor-studio/*

2. ���������� �������� blazor-����������� � ���������� ���������� Controls �� Radzen
3. ��� ������� � ���� ������ ������������ Entity Framework � ��������� SQLite. 
4. ��� ������� ���������� �������� ���� ���� ������ � �������� *wwwroot*
5. ��� ��������� ������ ������� �� ������� ����� ������������� ������ oxilor. ������ ������� ������ (������ 100) �� ������� ����� ����� ��������, ��������� �����

*https://data-api.oxilor.com/rest/regions?type=city&countryCode=RU&first=100*

6. � ���� ��� ������� ������� 10 ������� (���������� �������������� � ����� ��������)

## ��������� ���������:
1. SQLite ����� �������� � ������� �� ������� �����. ������� ���������� �� ������� ����� �� ��������� ����� � ������� ������� �� �������� (��� ������������� PostgreSQL ��� �������� ���������)

## ������������ ������

�������� �������� � ������������ ������ ���� �������

![Order page](Images/order_page.PNG)

�������� ������

![View order](Images/view_order.PNG)

����� ���������� ������

![Create order form](Images/create_order_form.PNG)

���������� ������ � �������

![Create order false 1](Images/create_order_false_1.PNG)

��� ���� �������� ������������ ��� ����������
![Create order false 2](Images/create_order_false_2.PNG)

�������� ���������� ������

![Create order true 1](Images/create_order_true_1.PNG)

![Create order true 2](Images/create_order_true_2.PNG)

������ ����������� � ����������

![filter example](Images/filter_example.PNG)


