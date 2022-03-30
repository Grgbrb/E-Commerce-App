import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-pagination-header',
  templateUrl: './pagination-header.component.html',
  styleUrls: ['./pagination-header.component.css']
})
export class PaginationHeaderComponent implements OnInit {
@Input() pageNumber: number;
@Input() totalCount: number;
@Input() pageSize: number;

  constructor() { }

  ngOnInit(): void {
  }

}
