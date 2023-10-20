import { Component, OnInit, TemplateRef, ViewChild, Input} from '@angular/core';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { NoteModelsComponent } from './note-models.component';
import { Category } from 'src/app/models/database/notes/category';
import { CategoryService } from 'src/app/services/models/notes/category.service';
import { MatSnackBar  } from '@angular/material/snack-bar';
import { FormControl } from '@angular/forms';
import { NgModel } from '@angular/forms';

@Component({
    selector: 'categories-app',
    templateUrl: './categories.component.html',
    providers: [ CategoryService ]
})
export class CategoriesComponent extends NoteModelsComponent<Category> {
    constructor(categoryService: CategoryService, snackBar: MatSnackBar){
        super(categoryService, snackBar);
    }

    @ViewChild('name') 
    nameModel: NgModel;

    protected override isValidModel(): boolean {
//         console.log(`
// this.nameModel?.invalid: ${this.nameModel?.invalid} 
// this.nameModel?.invalid ?? true: ${this.nameModel?.invalid  ?? true}
// this.editedModel: ${this.editedModel}
// !this.isEditedModel: ${!this.isEditedModel}
// (this.nameModel?.invalid ?? true) || !this.isEditedModel: ${(this.nameModel?.invalid ?? true) || !this.isEditedModel}
// !(this.nameModel?.invalid ?? true) || !this.isEditedModel: ${!(this.nameModel?.invalid ?? true) || !this.isEditedModel}

//         `)

        return !(this.nameModel?.invalid ?? true);
    }
    
}
