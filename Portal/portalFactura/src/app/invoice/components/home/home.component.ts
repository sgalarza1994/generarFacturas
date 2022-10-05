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
import { CompanyService } from 'src/app/services/company.service';
import { ICompany } from 'src/app/models/home/company.interface';
import { IInvoiceRequest } from 'src/app/models/home/invoice.interface';
import { ToastrService } from 'ngx-toastr';
import { IInvoiceDetail } from 'src/app/models/home/invoiceDetail.interface';
import { InvoiceDetailService } from 'src/app/services/invoice-detail.service';
import {MatTable} from '@angular/material/table';
import { Utilities } from 'src/app/util/utilities';
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
    private fb:FormBuilder,
    private companyService:CompanyService,
    private toashService:ToastrService,
    private invoiceServiceDetail:InvoiceDetailService
    ) { }
    listadoCompany:ICompany[] =[];
    listadoInvoiceDetalleLocal:IInvoiceDetail[] =[];
    listadoInvoiceDetalle = new MatTableDataSource<IInvoiceDetail>;
    invoiceSeleccionado:IInvoiceResponse | undefined;
    dataSource = new MatTableDataSource<IInvoiceResponse>;
    displayedColumns: string[] = ['clientName', 'invoiceNumber', 'companyName', 'total', 'action'];
    displayedColumnsDetalle: string[] = ['description', 'amount', 'unitPrice', 'tax', 'action'];
    //@ts-ignore
    @ViewChild(MatPaginator,{static:true}) paginator: MatPaginator;
    //@ts-ignore
    @ViewChild(MatSort,{static:true}) sort: MatSort;
    //@ts-ignore
    public modalRef: BsModalRef;
    //@ts-ignore
    @ViewChild(MatTable) tableDetalle: MatTable<IInvoiceDetail>;
    pdfSrc:any;
    config = {
      backdrop: true,
      ignoreBackdropClick: true,
      keyboard: false
    };
    formularioInvoice:FormGroup = new FormGroup({});
    formularioInvoiceDetalle:FormGroup = new FormGroup({});
  ngOnInit(): void {
    this.estructuraFormulario();
    this.getCompany();
  }

  ngAfterViewInit() {
    this.getInvoices();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  getCompany ()
  {
    let users = this.tokenService.getUser();
    let admin = users.rolId == 1;
    this.companyService.getCompany(users.userId,admin).subscribe({
      next :(response) =>
      {
        if(response)
        {
          if(response.success)
          {
            this.listadoCompany = response.result;
          }
        }
      },
      error :(erro:any) =>
      {

      }
    })
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
      companyId : [0,Validators.compose([Validators.required])]
    })

    this.formularioInvoiceDetalle = this.fb.group({
      description: ['',Validators.compose([Validators.required])],
      tax: [0,Validators.compose([Validators.required,Validators.min(1)])],
      unitPrice: [0,Validators.compose([Validators.required,Validators.min(1)])],
      amount: [0,Validators.compose([Validators.required,Validators.min(1)])],
    });
  }
  public openModalOpc(template: TemplateRef<any>, bandera: any,detalle:boolean) {
    if(detalle)
    {
      this.invoiceSeleccionado = bandera;
      this.getInvoiceDetalle(bandera.invoiceId);
    }
    this.modalRef = this.modalService.show(template, this.config);

  }

  getPdfFactura(invoiceId:number)
  {
    this.loaderService.isLoading.next(true);
    this.invoiceService.getPdf(invoiceId).subscribe({
      next : (response) =>
      {
         if(response.success)
         {
           console.log('response',response.message);
           let resposnes= Utilities.convertBase64ToBlob(response.message,"application/pdf");
           const blobUrl = URL.createObjectURL(resposnes);
        window.open(blobUrl, '_blank', 'toolbar=0,location=0,menubar=0');

         }
         else
         {
           this.toashService.error(response.message);
         }
      }
      ,error : (error:any) =>
      {

      }
      ,
      complete : () =>
      {
        this.loaderService.isLoading.next(false);
      }
    })
  }

  guardarFactura()
  {
    console.log('formulario',this.formularioInvoice.value);
    this.loaderService.isLoading.next(true);

    let request:IInvoiceRequest =
    {
      clientAddress: this.formularioInvoice.value.clientAddress,
      clientId : this.formularioInvoice.value.clientId,
      clientName : this.formularioInvoice.value.clientName,
      clientPhone : this.formularioInvoice.value.clientPhone,
      companyId : this.formularioInvoice.value.companyId,
      invoiceNumber : this.formularioInvoice.value.invoiceNumber

    };

    this.invoiceService.addInvoice(request).subscribe({
      next :(response) => {
        if(response)
        {
          if(response.success)
          {

            this.formularioInvoice.reset();
            this.getInvoices();
            this.modalRef.hide();
          }
          else
          {
            this.toashService.error(response.message);
          }
        }
      },
      error :(error:any) =>
      {

      },
      complete :() =>
      {
        this.loaderService.isLoading.next(false);
      }
    })
  }

  getInvoiceDetalle(id:number)
  {
    this.loaderService.isLoading.next(true);
    this.invoiceServiceDetail.getInvoice(id).subscribe({
      next :(response) =>
      {
        if(response)
        {
          if(response.success)
          {
            this.listadoInvoiceDetalleLocal = response.result;
            this.listadoInvoiceDetalle.data = response.result;
          }
        }
      },
      error :(error:any) =>
      {

      },
      complete :() =>
      {
        this.loaderService.isLoading.next(false);
      }
    })
  }

  agregarDetalle()
  {
    let detalle:IInvoiceDetail =
    {
      invoiceId : this.invoiceSeleccionado?.invoiceId!,
      amount : Number(this.formularioInvoiceDetalle.value.amount),
      description : this.formularioInvoiceDetalle.value.description,
      tax : Number(this.formularioInvoiceDetalle.value.tax),
      unitPrice : Number(this.formularioInvoiceDetalle.value.unitPrice),
      invoiceDetailId : 0
    };
    this.listadoInvoiceDetalleLocal.push(detalle);
    this.listadoInvoiceDetalle = new MatTableDataSource(this.listadoInvoiceDetalleLocal);


    this.tableDetalle.renderRows();
    console.log('listaoddeallte',this.listadoInvoiceDetalle);
  }

  guardarDetalle()
  {
    this.loaderService.isLoading.next(true);
    this.invoiceServiceDetail.addInvoiceDetail(this.listadoInvoiceDetalleLocal).subscribe({
      next :(response) =>{
        if(response)
        {
          if(response.success)
          {
            this.formularioInvoiceDetalle.reset();
            this.getInvoices();
            this.modalRef.hide();
          }
          else
          {
            this.toashService.error(response.message);
          }
        }
      },
      error :(error:any) =>
      {

      },
      complete :() =>
      {
        this.loaderService.isLoading.next(false);
      }
    })
  }

  eliminarRegistroTabla(item:IInvoiceDetail)
  {
    let indexUno = this.listadoInvoiceDetalle.data.indexOf(item);

    this.listadoInvoiceDetalleLocal.splice(indexUno,1);
    console.log('this.listadoInvoiceDetalleLocal',this.listadoInvoiceDetalleLocal);
    this.listadoInvoiceDetalle = new MatTableDataSource(this.listadoInvoiceDetalleLocal);
    this.tableDetalle.renderRows();
  }

  //#region GET DE LAS PROPIEDADES
  get clientName()
  {
   return this.formularioInvoice.get('clientName');
  }
  get clientId()
  {
   return this.formularioInvoice.get('clientId');
  }
  get clientAddress()
  {
   return this.formularioInvoice.get('clientAddress');
  }
  get clientPhone()
  {
   return this.formularioInvoice.get('clientPhone');
  }
  get invoiceNumber()
  {
   return this.formularioInvoice.get('invoiceNumber');
  }
  get companyId()
  {
   return this.formularioInvoice.get('companyId');
  }

  get tax()
  {
   return this.formularioInvoiceDetalle.get('tax');
  }
  get description()
  {
   return this.formularioInvoiceDetalle.get('description');
  }
  get amount()
  {
   return this.formularioInvoiceDetalle.get('amount');
  }
  get unitPrice()
  {
   return this.formularioInvoiceDetalle.get('unitPrice');
  }
  //#endregion

}
