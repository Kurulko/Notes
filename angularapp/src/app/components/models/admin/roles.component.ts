import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { Category } from 'src/app/models/database/notes/category';
import { CategoryService } from 'src/app/services/models/notes/category.service';
import { AdminModelsComponent } from './admin-models.component';
import { Role } from 'src/app/models/database/admin/role';
import { RoleService } from 'src/app/services/models/admin/role.service';
import { MatSnackBar  } from '@angular/material/snack-bar';

@Component({
    selector: 'roles-app',
    templateUrl: './roles.component.html',
    providers: [ RoleService ]
})
export class RolesComponent extends AdminModelsComponent<Role> {
    constructor(roleService: RoleService, snackBar: MatSnackBar){
        super(roleService, snackBar);
    }

    override createEmptyModel(): Role {
        return new Role();
    }
}
