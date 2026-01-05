import { Load } from '@/interfaces/Load';
import { Order } from '@/interfaces/Order';
import { User } from '@/interfaces/User';
import * as SecureStore from 'expo-secure-store';

export const API = {
    BASE_URL: 'http://192.168.100.11:5000/api',
    headers: {
        'Content-Type': 'application/json',
        Accept: 'application/json',
    }
}
const getAuthHeaders = async () => {
    const token = await SecureStore.getItemAsync('authToken')
    return { Authorization: `Bearer ${token}` }
}

export const getOrder = async (id: string) => {
    const endpoint = `${API.BASE_URL}/Order/${id}`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'GET',
        headers: {...API.headers, ...authHeaders
        },
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return await res.json()
}

export const getActiveOrders = async (): Promise<Order[]> => {
    const endpoint = `${API.BASE_URL}/Order/active-orders`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'GET',
        headers: {...API.headers, ...authHeaders},
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return await res.json()
}
export const getUserOrders = async (userId: string) => {
    const endpoint = `${API.BASE_URL}/Order/${userId}/orders`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'GET',
        headers: {...API.headers, ...authHeaders},
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return await res.json()
}
export const assignDriver = async (orderId: string, driverId: string) => {
    const endpoint = `${API.BASE_URL}/Order/${orderId}/assign-driver`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'PUT',
        headers: {...API.headers, ...authHeaders},
        body: JSON.stringify({ driverId }),
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return true
}
export const updateOrderStatus = async (orderId: string, status: number) => {
    const endpoint = `${API.BASE_URL}/Order/${orderId}/status`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'PUT',
        headers: {...API.headers, ...authHeaders},
        body: JSON.stringify({ status }),
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return true
}

export const updateOrder = async (orderId: string, payload: object) => {
    const endpoint = `${API.BASE_URL}/Order/${orderId}`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'PUT',
        headers: {...API.headers, ...authHeaders},
        body: JSON.stringify(payload),
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return await res.json()
}

export const registerUser = async (payload: {
    username: string
    name: string
    surname: string
    email: string
    password: string
}) => {
    const endpoint = `${API.BASE_URL}/user`
    const res = await fetch(endpoint, {
        method: 'POST',
        headers: API.headers,
        body: JSON.stringify(payload),
    })
    if (!res.ok) {
        if (res.status === 401) {
            throw new Error('Unauthorized')
        }
        throw new Error('Registration failed')
    }
    return await res.json()
}

export const loginUser = async (email: string, password: string) => {
    const endpoint = `${API.BASE_URL}/user/login`
    const res = await fetch(endpoint, {
        method: 'POST',
        headers: API.headers,
        body: JSON.stringify({ email, password }),
    })
    if (!res.ok) {
        if (res.status === 401) {
            throw new Error('Invalid email or password')
        }
        throw new Error('Login failed')
    }
    return await res.json()
}

export const checkTripStatus = async (tripId: string) => {
    const endpoint = `${API.BASE_URL}/trip/${tripId}/check-status`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'GET',        
        headers: {...API.headers, ...authHeaders},
        
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return await res.json()
}

export const startTrip = async (tripId: string) => {
    const endpoint = `${API.BASE_URL}/trip/${tripId}/start-trip`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'POST',        
        headers: {...API.headers, ...authHeaders},
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return res.status
}

export const addDriverNote = async (orderId: string, note: string, userId: string) => {
    const endpoint = `${API.BASE_URL}/Order/${orderId}/driver-note`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'POST',
        headers: {...API.headers, ...authHeaders},
        body: JSON.stringify({ note, userId }),
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return res.json()
}

export const uploadOrderDocument = async (
    orderId: string,
    file: { uri: string; name?: string; type?: string }
) => {
    const endpoint = `${API.BASE_URL}/Order/${orderId}/documents`
    const authHeaders = await getAuthHeaders()
    const formData = new FormData()
    formData.append('file', {
        uri: file.uri,
        name: file.name ?? 'file',
        type: file.type ?? 'application/octet-stream',
    } as any)

    const headers = {
        // Let fetch set the Content-Type with boundary for multipart/form-data
        Accept: 'application/json',
        ...authHeaders,
    }

    const res = await fetch(endpoint, {
        method: 'POST',
        headers,
        body: formData,
    })

    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return await res.json()
}

export const getUser = async (): Promise<User> => {
    const endpoint = `${API.BASE_URL}/user/me`
    const authHeaders = await getAuthHeaders()
    const res = await fetch(endpoint, {
        method: 'GET',
        headers: {...API.headers, ...authHeaders},
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return await res.json()
}