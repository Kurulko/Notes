import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Role } from '../../../models/database/admin/role';
import { Helpers } from '../../../helpers/helpers';
import { ModelsService } from '../models.service';
import { AdminModelService } from './admin-model.service';
import { Observable } from 'rxjs';

@Injectable()
export class RoleService extends AdminModelService<Role> {
    constructor(httpClient: HttpClient, helper: Helpers) {
        super(httpClient, helper, 'roles');
    }

    getRoleByName(name:string) : Observable<Role>{
        return this.returnModel(this.webClient.get<Role>(`by-name/${name}`));
    }
}
    