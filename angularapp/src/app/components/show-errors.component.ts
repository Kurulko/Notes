import { BaseComponent } from './base.component';
import { Component, Input } from '@angular/core';

@Component({
    selector: 'show-errors',
    templateUrl: './show-errors.component.html',
})
export class ShowErrorsComponent extends BaseComponent{
    @Input() 
    errors: string[] | null = null;
}