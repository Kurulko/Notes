import { AuthModel } from './auth-model';

export class RegisterModel extends AuthModel {
    email: string|null;
    passwordConfirm: string;
}