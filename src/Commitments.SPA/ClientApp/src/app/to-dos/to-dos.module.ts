import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { ToDoService } from './to-do.service';
import { ToDosPageComponent } from './to-dos-page.component';
import { CoreModule } from '../core/core.module';
import { SharedModule } from '../shared/shared.module';
import { EditToDoOverlayComponent } from './edit-to-do-overlay.component';

const declarations = [
  EditToDoOverlayComponent,
  ToDosPageComponent
];

const entryComponents = [
  EditToDoOverlayComponent
];

const providers = [
  ToDoService
];

@NgModule({
  declarations,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule,

    CoreModule,
    SharedModule
  ],
  providers,
  entryComponents
})
export class ToDosModule { }