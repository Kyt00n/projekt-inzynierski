import { createContext, ReactNode, useContext, useEffect, useState, useMemo } from "react";
import * as SecureStore from 'expo-secure-store';
import { loginUser } from "../services/api";

interface AuthContextType {
    authState?: {token: string | null; authenticated: boolean|null};
    onLogin?: (email: string, password: string) => Promise<any>;
    onLogout?: () => void;
}

const AuthContext = createContext<AuthContextType>({});

const TOKEN_KEY = 'authToken';

export const AuthProvider = ({ children }: { children: ReactNode }) => {
    const [authState, setAuthState] = useState<{token: string | null; authenticated: boolean|null}>({ token: null, authenticated: null });
    
    useEffect(() => {
        const loadAuthState = async () => {
            const token = await SecureStore.getItemAsync(TOKEN_KEY);
            if (token){
            setAuthState({ token, authenticated: true });            
            }
        };
        loadAuthState();
    }, []);

    const login = async (email: string, password: string) => {
        const jwtToken = await loginUser(email, password);
        await SecureStore.setItemAsync(TOKEN_KEY, jwtToken.token);
        setAuthState({ token: jwtToken.token, authenticated: true });
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