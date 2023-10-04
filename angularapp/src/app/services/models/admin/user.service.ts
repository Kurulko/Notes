import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdminModelService } from './admin-model.service';
import { User } from 'src/app/models/database/admin/user';
import { Helpers } from 'src/app/helpers/helpers';

@Injectable()
export class UserService extends AdminModelService<User> {
    constructor(httpClient: HttpClient, helper: Helpers) {
        super(httpClient, helper, 'users');
    }
}
    