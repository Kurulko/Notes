import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { Category } from 'src/app/models/database/notes/category';
import { CategoryService } from 'src/app/services/models/notes/category.service';
import { AdminModelsComponent } from './admin-models.component';
import { Role } from 'src/app/models/database/admin/role';
import { RoleService } from 'src/app/services/models/admin/role.service';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'roles-app',
    templateUrl: './roles.component.html',
    providers: [ RoleService ]
})
export class RolesComponent extends AdminModelsComponent<Role> {
    constructor(router: Router, roleService: RoleService, route: ActivatedRoute, snackBar: MatSnackBar){
        super(router, roleService, route, snackBar);
    }

    @ViewChild('name') 
    nameModel: NgModel;

    protected override isValidModel(): boolean {
        return !(this.nameModel?.invalid ?? true);
    }
}
