1) загрузить проект в Visual Studio

2) В зависимости app.module.ts добавить и подключить соотв. классы в declarations:

****************************************************************
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
****************************************************************

3)В app.component.html добавить:

<gr-login></gr-login> - для страницы с логином
<gr-signup></gr-signup> - для страницы с регистрацией

4) Запустить проект



Note: В signup.component.css и login.component.css без класса wrapper(там и height 100vh)
страница отображается в пол экрана.