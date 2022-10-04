export interface IInvoiceResponse 
{
    invoiceId: number;
    clientName: string;
    clientId: string;
    clientAddress: string;
    clientPhone: string;
    invoiceNumber: string;
    companyId: number;
    companyName: string;
    total: number;
}
