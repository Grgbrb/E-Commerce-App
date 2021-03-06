import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private shopService: ShopService,private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadProduct();
  }
//With the Activated Route I can take the correct id of an object
  loadProduct(){
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe({next:(product) =>
      {
        this.product = product;
      },
      error : (e) => 
      {
        console.error(e);
      }
    })
  }
}
