1) ��������� ������ � Visual Studio

2) � ����������� app.module.ts �������� � ���������� �����. ������ � declarations:

****************************************************************
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
****************************************************************

3)� app.component.html ��������:

<gr-login></gr-login> - ��� �������� � �������
<gr-signup></gr-signup> - ��� �������� � ������������

4) ��������� ������



Note: � signup.component.css � login.component.css ��� ������ wrapper(��� � height 100vh)
�������� ������������ � ��� ������.