import { Input, Component } from "@angular/core";

@Component({
    selector: 'app-left-panel-layout',
    templateUrl: './left-panel-layout.component.html',
    styleUrls: ['./left-panel-layout.component.css']
})
export class LeftPanelLayoutComponent {
    @Input() 
    isAuthenticated:boolean = false;
}