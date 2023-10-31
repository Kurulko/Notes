import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { Category } from 'src/app/models/database/notes/category';
import { CategoryService } from 'src/app/services/models/notes/category.service';
import { AdminModelsComponent } from './admin-models.component';
import { Role } from 'src/app/models/database/admin/role';
import { RoleService } from 'src/app/services/models/admin/role.service';
import { User } from 'src/app/models/database/admin/user';
import { UserService } from 'src/app/services/models/admin/user.service';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'users-app',
    templateUrl: './users.component.html',
    providers: [ UserService ]
})
export class UsersComponent extends AdminModelsComponent<User> {
    constructor(router: Router, userService: UserService, route: ActivatedRoute, snackBar: MatSnackBar){
        super(router, userService, route, snackBar);
    }

    @ViewChild('userName') 
    userNameModel: NgModel;

    @ViewChild('email') 
    emailModel: NgModel;

    protected override isValidModel(): boolean {
        return !(this.userNameModel?.invalid || this.emailModel?.invalid);
    }
}
