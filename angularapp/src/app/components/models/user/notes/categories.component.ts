import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NoteItemService } from 'src/app/services/models/notes/note-item.service';
import { NoteModelsComponent } from './note-models.component';
import { Category } from 'src/app/models/database/notes/category';
import { CategoryService } from 'src/app/services/models/notes/category.service';

@Component({
    selector: 'categories-app',
    templateUrl: './categories.component.html',
    providers: [ CategoryService ]
})
export class CategoriesComponent extends NoteModelsComponent<Category> {
    constructor(categoryService: CategoryService){
        super(categoryService);
    }

    override createEmptyModel(): Category {
        return new Category();
    }
}