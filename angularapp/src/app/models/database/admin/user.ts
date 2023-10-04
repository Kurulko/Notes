export class User extends AdminModel {
    userName: string;
    usedUserId: string|null;
    email: string|null;
    registered: Date;
}