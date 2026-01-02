import { Load } from "./Load";

export interface Order {
    id: string;
    userId: string;
    pickupLocation: string;
    deliveryLocation: string;
    itemDescription: string;
    specialInstructions?: string;
    driverNotes?: string;
    status: string;
    createdAt: string;
    updatedAt: string;
    totalLoads: number;
    totalWeight: number;
    loads: Load[];
}