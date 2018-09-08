import { AbstractControl, FormGroup } from '@angular/forms';
export class PasswordValidation {

  static PasswordsMatch(form: FormGroup) {
    let password = form.get('password').value;
    let confirmPassword = form.get('confirmPassword').value;
    return password == confirmPassword ? undefined : { 'PasswordsDoNotMatch': true }
  }
}
