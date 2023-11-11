import { Component, OnInit, TemplateRef, ViewChild, Input} from '@angular/core';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { NoteModelsComponent } from './note-models.component';
import { Category } from 'src/app/models/database/notes/category';
import { CategoryService } from 'src/app/services/models/notes/category.service';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { FormControl } from '@angular/forms';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { UserService } from 'src/app/services/models/admin/user.service';
import { Observable } from 'rxjs';
import { IndexViewModel } from 'src/app/models/helpers/index-view-model';

@Component({
    selector: 'categories-app',
    templateUrl: './categories.component.html',
    providers: [ CategoryService, UserService  ]
})
export class CategoriesComponent extends NoteModelsComponent<Category> {
    constructor(router: Router, userService: UserService, categoryService: CategoryService, route: ActivatedRoute, snackBar: MatSnackBar){
        super(router, userService, categoryService, route, snackBar);
    }

    @ViewChild('name') 
    nameModel: NgModel;

    protected override isValidModel(): boolean {
        return !(this.nameModel?.invalid ?? true);
    }
 
    protected override getUserModels(attribute?: string | undefined, orderBy?: string | undefined, pageNumber?: number | undefined, pageSize?: number | undefined): Observable<IndexViewModel<Category>> {
        return this.userService.getUserCategories(attribute, orderBy, pageNumber, pageSize);
    }
}
