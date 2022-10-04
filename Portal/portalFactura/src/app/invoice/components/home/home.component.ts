import { Component, OnInit,ViewChild ,AfterViewInit,TemplateRef} from '@angular/core';
import { LoaderService } from 'src/app/core/services/loader.service';
import { InvoiceService } from 'src/app/services/invoice.service';
import { IResponse,Response } from 'src/app/models/generic/response.interface';
import { IInvoiceResponse } from 'src/app/models/home/invoiceresponse.interface';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { TokenService } from 'src/app/core/services/token.service';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements AfterViewInit {

  constructor(private invoiceService:InvoiceService,
    private loaderService:LoaderService,
    private tokenService:TokenService,
    private modalService: BsModalService,
    private fb:FormBuilder
    ) { }
    dataSource = new MatTableDataSource<IInvoiceResponse>;
    displayedColumns: string[] = ['clientName', 'invoiceNumber', 'companyName', 'total', 'action'];
    //@ts-ignore
    @ViewChild(MatPaginator,{static:true}) paginator: MatPaginator;
    //@ts-ignore
    @ViewChild(MatSort,{static:true}) sort: MatSort;
    //@ts-ignore
    public modalRef: BsModalRef;
    config = {
      backdrop: true,
      ignoreBackdropClick: true,
      keyboard: false
    };
    formularioInvoice:FormGroup = new FormGroup({});
  ngOnInit(): void {
  }

  ngAfterViewInit() {
    this.getInvoices();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }


  getInvoices()
  {
    let users = this.tokenService.getUser();
    let admin = users.rolId == 1;
  //  this.loaderService.isLoading.next(true);
    this.invoiceService.getInvoice(users.userId,admin).subscribe({
      next : (response) => 
      {
         if(response)
         {
          if(response.success)
          {
            console.info('respuestaResult',JSON.stringify(response.result));
            this.dataSource.data = response.result;
          }
         }
      },
      error : (error:any) =>
      {

      },
      complete : ()  => 
      {
      //  this.loaderService.isLoading.next(false);
      }
    })

  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  estructuraFormulario()
  {
    this.formularioInvoice = this.fb.group({
      clientName: ['',Validators.compose([Validators.required])],
      clientId : ['',Validators.compose([Validators.required])],
      clientAddress : [],
      clientPhone : [],
      invoiceNumber : ['',Validators.compose([Validators.required])],
      companayId : ['',Validators.compose([Validators.required])]
    })
  }
  public openModalOpc(template: TemplateRef<any>, bandera: any) {
    this.modalRef = this.modalService.show(template, this.config);

  }

}
