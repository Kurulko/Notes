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
import { NoteModel } from 'src/app/models/database/notes/note-model';

@Injectable()
export class UserService extends AdminModelService<User> {
    constructor(httpClient: HttpClient, helper: Helpers) {
        super(httpClient, helper, 'users');
    }

    getUserIdByUserName(userName:string) : Observable<string>{
        return this.returnModel(this.webClient.get<string>(`userid-by-name/${userName}`));
    }

    getCurrentUserName() : Observable<string>{
        return this.returnModel(this.webClient.get<string>(`current-username`));
    }

    isImpersonating() : Observable<boolean>{
        return this.returnModel(this.webClient.get<boolean>(`is-impersonating`));
    }

    getUserByName(name:string) : Observable<User>{
        return this.returnModel(this.webClient.get<User>(`by-name/${name}`));
    }

    getCurretUser() : Observable<User>{
        return this.returnModel(this.webClient.get<User>(`current`));
    }

    getUserByUserName(userName:string) : Observable<User>{
        return this.returnModel(this.webClient.get<User>(`name/${userName}`));
    }

    getUsedUser() : Observable<User>{
        return this.returnModel(this.webClient.get<User>(`usedUser`));
    }

    changeUsedUserId(usedUserId: string) : Observable<Object>{
        return this.returnModel(this.webClient.put(`change-used-userId`, usedUserId));
    }

    dropUsedUserId() : Observable<Object>{
        return this.returnModel(this.webClient.delete(`drop-used-userId`));
    }

    private getUserModels<T extends NoteModel>(path:string, attribute?:string, orderBy?:string, pageNumber?:number, pageSize?:number, userId?:string) : Observable<IndexViewModel<T>>{
        return this.returnModel(this.webClient.get<IndexViewModel<T>>(this.sortAndPadingUrlStr(`${path}/${userId ?? ''}`, attribute, orderBy, pageNumber, pageSize)));
    }
    
    getUserNoteItems(attribute?:string, orderBy?:string, pageNumber?:number, pageSize?:number, userId?:string) : Observable<IndexViewModel<NoteItem>>{
        return this.getUserModels(`user-noteitems`, attribute, orderBy, pageNumber, pageSize, userId);
    }

    hasPassword(userId:string) : Observable<boolean>{
        return this.returnModel(this.webClient.get<boolean>(`${userId}/password`));
    }

    changePassword(changePassword: ChangePassword) : Observable<Object>{
        return this.returnModel(this.webClient.put(`password`, changePassword));
    }

    createPassword(modelWithUserId: ModelWithUserId<string>) : Observable<Object>{
        return this.returnModel(this.webClient.post(`password`, modelWithUserId));
    }

    getRoles(userId?:string) : Observable<Role[]>{
        return this.returnModel(this.webClient.get<Role[]>(`user-roles/${userId ?? ''}`));
    }

    addRole(userId:string, role:string) : Observable<Object>{
        return this.returnModel(this.webClient.post(`${userId}/role`, role));
    }

    deleteRole(userId:string, role:string) : Observable<Object>{
        return this.returnModel(this.webClient.delete(`${userId}/${role}`));
    }

    getUserByRole(role:string) : Observable<User[]>{
        return this.returnModel(this.webClient.get<User[]>(`users-by-role/${role}`));
    }
}
    