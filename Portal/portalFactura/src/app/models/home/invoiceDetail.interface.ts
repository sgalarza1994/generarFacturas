export interface IInvoiceDetail 
{
    invoiceDetailId: number;
    invoiceId: number;
    description: string;
    amount: number;
    unitPrice: number;
    tax: number;
}
