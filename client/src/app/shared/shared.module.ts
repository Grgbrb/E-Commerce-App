import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PaginationHeaderComponent } from './Components/pagination-header/pagination-header.component';
import { PagerComponent } from './Components/pager/pager.component';



@NgModule({
  declarations: [
    PaginationHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
  ],
  exports:[
    PaginationModule,
    PaginationHeaderComponent,
    PagerComponent
  ]
})
export class SharedModule { }
