import React, { useState } from 'react'
import { View, Text, TextInput, TouchableOpacity, KeyboardAvoidingView, Platform, Alert } from 'react-native'
import { useRouter } from 'expo-router'
import { useAuth } from './authProvider'

export default function Login() {
  const router = useRouter()
  const [email, setEmail] = useState<string>('')
  const [password, setPassword] = useState<string>('')
  const [loading, setLoading] = useState<boolean>(false)
  const { onLogin } = useAuth()

  const handleLogin = async () => {
    if (!email || !password) {
      Alert.alert('Error', 'Please enter email and password')
      return
    }

    try {
      setLoading(true)
      await onLogin?.(email, password)
      router.push('/(tabs)')
    } catch (error) {
      const message = error instanceof Error ? error.message : 'Login failed'
      Alert.alert('Error', message)
    } finally {
      setLoading(false)
    }
  }

  const handleSignUp = () => {
    router.push('/signIn')
  }

  return (
    <KeyboardAvoidingView
      behavior={Platform.OS === 'ios' ? 'padding' : 'height'}
      className="flex-1 bg-primary"
    >
      <View className="flex-1 px-8 justify-center">
        <Text className="text-white text-5xl font-bold text-center mb-12">
          LTL Manager
        </Text>

        <View className="mb-4">
          <Text className="text-gray-400 text-sm mb-2">Email</Text>
          <TextInput
            className="bg-gray-800 text-white px-4 py-3 rounded-lg"
            placeholder="Enter your email"
            placeholderTextColor="#9CA3AF"
            value={email}
            onChangeText={setEmail}
            keyboardType="email-address"
            autoCapitalize="none"
            autoCorrect={false}
          />
        </View>

        <View className="mb-8">
          <Text className="text-gray-400 text-sm mb-2">Password</Text>
          <TextInput
            className="bg-gray-800 text-white px-4 py-3 rounded-lg"
            placeholder="Enter your password"
            placeholderTextColor="#9CA3AF"
            value={password}
            onChangeText={setPassword}
            secureTextEntry
            autoCapitalize="none"
            autoCorrect={false}
          />
        </View>

        <TouchableOpacity
          className="bg-blue-600 py-4 rounded-lg mb-4"
          onPress={handleLogin}
          disabled={loading}
        >
          <Text className="text-white text-center font-semibold text-base">
            {loading ? 'Logging in...' : 'Login'}
          </Text>
        </TouchableOpacity>

        <TouchableOpacity
          className="bg-gray-700 py-4 rounded-lg"
          onPress={handleSignUp}
        >
          <Text className="text-white text-center font-semibold text-base">
            Sign Up
          </Text>
        </TouchableOpacity>
      </View>
    </KeyboardAvoidingView>
  )
}
