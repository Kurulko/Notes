import { Component, OnInit, TemplateRef, ViewChild, Input} from '@angular/core';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { NoteModelsComponent } from './note-models.component';
import { Category } from 'src/app/models/database/notes/category';
import { CategoryService } from 'src/app/services/models/notes/category.service';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { FormControl } from '@angular/forms';
import { NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'categories-app',
    templateUrl: './categories.component.html',
    providers: [ CategoryService ]
})
export class CategoriesComponent extends NoteModelsComponent<Category> {
    constructor(router: Router, categoryService: CategoryService, route: ActivatedRoute, snackBar: MatSnackBar){
        super(router, categoryService, route, snackBar);
    }

    @ViewChild('name') 
    nameModel: NgModel;

    protected override isValidModel(): boolean {
        return !(this.nameModel?.invalid ?? true);
    }
    
}
