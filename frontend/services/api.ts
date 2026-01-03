import { Load } from '@/interfaces/Load';
import { Order } from '@/interfaces/Order';
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
    const res = await fetch(endpoint, {
        method: 'GET',
        headers: API.headers,
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return await res.json()
}
export const assignDriver = async (orderId: string, driverId: string) => {
    const endpoint = `${API.BASE_URL}/Order/${orderId}/assign-driver`
    const res = await fetch(endpoint, {
        method: 'PUT',
        headers: API.headers,
        body: JSON.stringify({ driverId }),
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return true
}
export const updateOrderStatus = async (orderId: string, status: string) => {
    const endpoint = `${API.BASE_URL}/Order/${orderId}/status`
    const res = await fetch(endpoint, {
        method: 'PUT',
        headers: API.headers,
        body: JSON.stringify({ status }),
    })
    if (!res.ok) {
        throw new Error('Network response was not ok')
    }
    return true
}

export const updateOrder = async (orderId: string, payload: object) => {
    const endpoint = `${API.BASE_URL}/Order/${orderId}`
    const res = await fetch(endpoint, {
        method: 'PUT',
        headers: API.headers,
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

// const getPhotos = (id: number) => {
//     const photos: string[] = [];
//     for (let i = 0; i < 6; i++) {
//         photos.push(`https://picsum.photos/id/${id + (5 * i)}/200/300`);
//     }
//     return photos;
// };
// export const getPosts = async ({query}: {query: string}) => {
//     const endpoint = query ?
//      `${JsonPlaceholderAPI.BASE_URL}/posts?title_like=${query}` :
//       `${JsonPlaceholderAPI.BASE_URL}/posts`

//     const response = await fetch(endpoint, {
//         method: 'GET',
//         headers: JsonPlaceholderAPI.headers,
//     })
//     if (!response.ok) {
//         throw new Error('Network response was not ok')
//     }
//     const posts = await response.json()
//     const postsWithThumbnails = posts.map((post: any) => {
//         const thumbnailUrl = `https://picsum.photos/id/${post.id}/200/300`;
//         return { ...post, thumbnailUrl };
//     });
    

//     return postsWithThumbnails;
// }

// export const getPostById = async (id: string) => {
//     const endpoint = `${JsonPlaceholderAPI.BASE_URL}/posts/${id}`

//     const response = await fetch(endpoint, {
//         method: 'GET',
//         headers: JsonPlaceholderAPI.headers,
//     })
//     if (!response.ok) {
//         throw new Error('Network response was not ok')
//     }
//     const post = await response.json()
//     const thumbnailUrl = `https://picsum.photos/id/${post.id}/200/300`;
//     const photos = getPhotos(post.id);
//     const postWithThumbnail = { ...post, thumbnailUrl, photos };
//     return postWithThumbnail
// }

// export const getUserById = async (id: string) => {
//     const endpoint = `${JsonPlaceholderAPI.BASE_URL}/users/${id}`

//     const response = await fetch(endpoint, {
//         method: 'GET',
//         headers: JsonPlaceholderAPI.headers,
//     })
//     if (!response.ok) {
//         throw new Error('Network response was not ok')
//     }
//     const user = await response.json()
//     return user
// }

// export const getPhotosByPost = async ({postId, photoId}: {postId:number, photoId?: number}) => {
//     const endpoint = photoId ?
//      `${JsonPlaceholderAPI.BASE_URL}/posts/${postId}/photos?id=${photoId}` :
//       `${JsonPlaceholderAPI.BASE_URL}/posts/${postId}/photos`

//     const response = await fetch(endpoint, {
//         method: 'GET',
//         headers: JsonPlaceholderAPI.headers,
//     })
//     if (!response.ok) {
//         throw new Error('Network response was not ok')
//     }
//     const data = await response.json()
//     return data
// }
// export const getSavedPosts = async ({userId}: {userId: string}) => {
//     const endpoint = `${JsonPlaceholderAPI.BASE_URL}/users/${userId}/posts`

//     const response = await fetch(endpoint, {
//         method: 'GET',
//         headers: JsonPlaceholderAPI.headers,
//     })
//     if (!response.ok) {
//         throw new Error('Network response was not ok')
//     }
//     const posts = await response.json()
//     const postsWithThumbnails = posts.map((post: any) => {
//         const thumbnailUrl = `https://picsum.photos/id/${post.id}/200/300`;
//         return { ...post, thumbnailUrl };
//     });
    

//     return postsWithThumbnails;
// }

