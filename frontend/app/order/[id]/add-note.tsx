import React, { useState } from 'react'
import { View, Text, TextInput, TouchableOpacity, ActivityIndicator, Alert } from 'react-native'
import { useLocalSearchParams, useRouter } from 'expo-router'
import { addDriverNote } from '@/services/api'
import { useAuth } from '@/app/authProvider'

export default function AddNote() {
  const { id } = useLocalSearchParams()
  const router = useRouter()
  const [note, setNote] = useState('')
  const [loading, setLoading] = useState(false)

  const { authState } = useAuth();
  
  const submit = async () => {
    if (!id) return Alert.alert('Error', 'Order id missing')
    if (!note.trim()) return Alert.alert('Validation', 'Please enter a note')
    try {
      setLoading(true)
      await addDriverNote(id as string, note.trim(), authState?.userId as string)
      Alert.alert('Success', 'Note added', [{ text: 'OK', onPress: () => router.back() }])
    } catch (err: any) {
      console.error('addDriverNote error', err)
      Alert.alert('Error', err?.message ?? String(err))
    } finally {
      setLoading(false)
    }
  }

  return (
    <View className="flex-1 bg-primary px-5" style={{ paddingTop: 60 }}>
      <Text className="text-white text-xl font-bold mb-4">Add driver note</Text>
      <TextInput
        value={note}
        onChangeText={setNote}
        multiline
        numberOfLines={6}
        placeholder="Enter note..."
        placeholderTextColor="#9CA3AF"
        className="bg-gray-800 text-white p-3 rounded-lg"
        style={{ textAlignVertical: 'top' }}
      />

      <View className="mt-4">
        <TouchableOpacity
          className="bg-blue-600 rounded-xl py-3 items-center"
          onPress={submit}
          disabled={loading}
        >
          {loading ? <ActivityIndicator color="#fff" /> : <Text className="text-white font-semibold">Send note</Text>}
        </TouchableOpacity>
      </View>
    </View>
  )
}
