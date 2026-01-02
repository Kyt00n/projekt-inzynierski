import { Stack } from "expo-router";
import './globals.css';
import { AuthProvider, useAuth } from "./authProvider";
const InitialLayout = () => {
  const { authState } = useAuth();

  return (
<Stack>
        <Stack.Protected guard={!!authState?.authenticated}>
          <Stack.Screen
            name="(tabs)"
            options={{
              headerShown: false,
            }}
          />
          <Stack.Screen
            name="order/[id]"
            options={{
              headerShown: false,
            }}
          />
        </Stack.Protected>
        <Stack.Screen
          name="login"
          options={{
            headerShown: false,
          }}
        />
        <Stack.Screen
          name="signIn"
          options={{
            headerShown: false,
          }}
        />
      </Stack>
  )
}
export default function RootLayout() {
  return (
    <AuthProvider>
      <InitialLayout />
    </AuthProvider>
  );
}
