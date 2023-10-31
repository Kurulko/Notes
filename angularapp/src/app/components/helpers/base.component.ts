import { Component, Input } from '@angular/core';

@Component({
    selector: 'base-app',
    templateUrl: './base.component.html',
})
export class BaseComponent{
    @Input() title: string = "";
}