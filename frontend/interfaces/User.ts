export interface User {
    id: number;
    name: string;
    username: string;
    email: string;
    isActive: boolean;
    phone: string;
    website: string;
    company: {
        name: string;
        catchPhrase: string;
        bs: string;
    };
}