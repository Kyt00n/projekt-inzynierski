import { Load } from "./Load";

export interface Order {
    orderId: string;
    userId: string;
    pickupLocation: string;
    deliveryLocation: string;
    itemDescription: string;
    specialInstructions?: string;
    driverNotes?: string;
    status: number;
    createdAt: string;
    updatedAt: string;
    loads: Load[];
}