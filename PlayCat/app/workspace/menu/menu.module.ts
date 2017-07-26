import { NgModule } from '@angular/core';
import { MenuComponent } from './menu.component';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        RouterModule
    ],
    declarations: [
        MenuComponent,
    ],
    exports: [
        MenuComponent,
    ],
})
export class MenuModule {

}