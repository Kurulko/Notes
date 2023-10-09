import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AdminModelService } from './admin-model.service';
import { User } from 'src/app/models/database/admin/user';
import { Helpers } from 'src/app/helpers/helpers';
import { Observable } from 'rxjs';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';
import { NoteItem } from 'src/app/models/database/notes/note-item';
import { ChangePassword } from 'src/app/models/helpers/change-password';
import { ModelWithUserId } from 'src/app/models/helpers/model-with-userid';
import { Role } from 'src/app/models/database/admin/role';

@Injectable()
export class UserService extends AdminModelService<User> {
    constructor(httpClient: HttpClient, helper: Helpers) {
        super(httpClient, helper, 'users');
    }

    getUserIdByUserName(userName:string) : Observable<string>{
        return this.webClient.get<string>(`userid-by-name/${userName}`)
    }

    getCurrentUserName() : Observable<string>{
        return this.webClient.get<string>(`current-username`)
    }

    isImpersonating() : Observable<boolean>{
        return this.webClient.get<boolean>(`is-impersonating`)
    }

    getUserByName(name:string) : Observable<User>{
        return this.webClient.get<User>(`by-name/${name}`)
    }

    getCurretUser() : Observable<User>{
        return this.webClient.get<User>(`current`)
    }

    getUserByUserName(userName:string) : Observable<User>{
        return this.webClient.get<User>(`name/${userName}`)
    }

    getUsedUser() : Observable<User>{
        return this.webClient.get<User>(`usedUser`)
    }

    changeUsedUserId(usedUserId: string) : Observable<Object>{
        return this.webClient.put(`change-used-userId`, usedUserId)
    }

    dropUsedUserId() : Observable<Object>{
        return this.webClient.delete(`drop-used-userId`)
    }

    getUserNoteItems(attribute?:string, orderBy?:string, pageNumber?:number, pageSize?:number, userId?:string) : Observable<IndexViewModel<NoteItem>>{
        return this.webClient.get<IndexViewModel<NoteItem>>(`user-noteitems/${userId ?? ''}?attribute=${attribute}&orderBy=${orderBy}&pageNumber=${pageNumber}&pageSize=${pageSize}`)
    }

    hasPassword(userId:string) : Observable<boolean>{
        return this.webClient.get<boolean>(`${userId}/password`)
    }

    changePassword(changePassword: ChangePassword) : Observable<Object>{
        return this.webClient.put(`change-used-userId`, changePassword)
    }

    createPassword(modelWithUserId: ModelWithUserId<string>) : Observable<Object>{
        return this.webClient.post(`change-used-userId`, modelWithUserId)
    }

    getRoles(userId?:string) : Observable<Role[]>{
        return this.webClient.get<Role[]>(`user-roles/${userId ?? ''}`)
    }

    addRole(userId:string, role:string) : Observable<Object>{
        return this.webClient.post(`${userId}/role`, role)
    }

    deleteRole(userId:string, role:string) : Observable<Object>{
        return this.webClient.delete(`${userId}/${role}`)
    }

    getUserByRole(role:string) : Observable<User[]>{
        return this.webClient.get<User[]>(`users-by-role/${role}`)
    }
}
    