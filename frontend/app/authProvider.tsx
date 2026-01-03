import { createContext, ReactNode, useContext, useEffect, useState, useMemo } from "react";
import * as SecureStore from 'expo-secure-store';
import { loginUser } from "../services/api";

interface AuthContextType {
    authState?: { token: string | null; authenticated: boolean | null; userId?: string | null };
    onLogin?: (email: string, password: string) => Promise<any>;
    onLogout?: () => void;
}

const AuthContext = createContext<AuthContextType>({});

const TOKEN_KEY = 'authToken';
const parseJwt = (token: string | null) => {
    if (!token) return null;
    try {
        // token may be stored as "<jwt>-<salt>", take the jwt part
        const jwtPart = token.split('-')[0];
        const base64Url = jwtPart.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const binary = typeof atob === 'function'
            ? atob(base64)
            : /* fallback for environments with Buffer */ Buffer.from(base64, 'base64').toString('binary');
        const json = decodeURIComponent(Array.prototype.map.call(binary, (c: any) => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)).join(''));
        return JSON.parse(json);
    } catch {
        return null;
    }
};
export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [authState, setAuthState] = useState<{ token: string | null; authenticated: boolean | null; userId?: string | null }>({ token: null, authenticated: null });
    
    useEffect(() => {
        const loadAuthState = async () => {
            const stored = await SecureStore.getItemAsync(TOKEN_KEY);
            if (stored) {
                const payload = parseJwt(stored);
                const userId = payload?.sub ?? payload?.userId ?? null;
                setAuthState({ token: stored, authenticated: true, userId });
            }
        };
        loadAuthState();
    }, []);

    const login = async (email: string, password: string) => {
        const jwtResp = await loginUser(email, password);
        const tokenValue = jwtResp?.token ?? jwtResp?.accessToken ?? jwtResp?.jwt ?? null;
        if (!tokenValue) throw new Error('No token returned from login');
        await SecureStore.setItemAsync(TOKEN_KEY, tokenValue);
        const payload = parseJwt(tokenValue);
        const userId = payload?.sub ?? payload?.userId ?? null;
        setAuthState({ token: tokenValue, authenticated: true, userId });
        return jwtResp;
    };

    const logout = async () => {
        await SecureStore.deleteItemAsync(TOKEN_KEY);
        setAuthState({ token: null, authenticated: false });
    };
    const value = {
        onLogin: login,
        onLogout: logout,
        authState,
    }

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );

};

export const useAuth = () => {
    return useContext(AuthContext);
};

export default AuthProvider;