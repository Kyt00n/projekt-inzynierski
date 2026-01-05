import { ActivityIndicator, ScrollView, Text, TouchableOpacity, View, Alert } from 'react-native'
import React, { useEffect, useState } from 'react'
import { useRouter } from 'expo-router'
import { useLocalSearchParams } from 'expo-router'
import { Image, ScrollView as HorizontalScrollView } from 'react-native'
import * as ImagePicker from 'expo-image-picker'
import * as DocumentPicker from 'expo-document-picker'
import { uploadOrderDocument } from '@/services/api'
import { Load } from '@/interfaces/Load'
import { assignDriver, getOrder, updateOrderStatus } from '@/services/api'
import { Order } from '@/interfaces/Order'
import { useAuth } from '../authProvider'

const OrderDetails = () => {
  const { id } = useLocalSearchParams()
    
  const [order, setOrder] = useState<Order | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)
  const {authState} = useAuth();
  const router = useRouter();

  useEffect(() => {
    const fetchOrder = async () => {
      try {
        setLoading(true)
        const fetched = await getOrder(id as string)
        setOrder(fetched)
      } catch (err) {
        setError(err instanceof Error ? err.message : 'Failed to fetch order')
      } finally {
        setLoading(false)
      }
    }
    if (id) {
      fetchOrder()
    }
  }, [id])

  const handleApply = async () => {
    try {
      setLoading(true)
      await assignDriver(order?.orderId as string, authState?.userId as string)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to assign driver')
    } finally {
      setLoading(false)
    }
  }

  const handleAddNote = async () => {
    if (!order?.orderId) return
    router.push(`/order/${order.orderId}/add-note`)
  }

  const handleAddDocuments = async () => {
    if (!order?.orderId) return

    const takePhoto = async () => {
      try {
        setLoading(true)
        const camPerm = await ImagePicker.requestCameraPermissionsAsync()
        if (camPerm.status !== 'granted') {
          Alert.alert('Permission required', 'Camera permission is required to take a photo')
          return
        }
        const res = await ImagePicker.launchCameraAsync({ allowsEditing: false, quality: 0.8 })
        if (res.canceled) return
        console.log(res.assets[0])
        const file = { uri: res.assets[0].uri, name: res.assets[0].uri.split('/').pop() ?? 'photo.jpg', type: 'image/jpeg' }
        await uploadOrderDocument(order.orderId as string, file)
        Alert.alert('Success', 'Document uploaded')
      } catch (err) {
        Alert.alert('Upload failed', err instanceof Error ? err.message : 'Unknown error')
      } finally {
        setLoading(false)
      }
    }

    const chooseFile = async () => {
      try {
        setLoading(true)
        const camPerm = await ImagePicker.requestMediaLibraryPermissionsAsync()
        if (camPerm.status !== 'granted') {
          Alert.alert('Permission required', 'Camera permission is required to take a photo')
          return
        }
        const res = await ImagePicker.launchImageLibraryAsync({ allowsEditing: false, quality: 0.8, allowsMultipleSelection: true })
        if (res.canceled) return
        for (const asset of res.assets) {
          console.log('Uploading asset', asset)
          const file = { uri: asset.uri, name: asset.uri.split('/').pop() ?? 'photo.jpg', type: 'image/jpeg' }
          await uploadOrderDocument(order.orderId as string, file)
        }
        Alert.alert('Success', `Uploaded ${res.assets.length} file${res.assets.length > 1 ? 's' : ''}`)
      } catch (err) {
        Alert.alert('Upload failed', err instanceof Error ? err.message : 'Unknown error')
      } finally {
        setLoading(false)
      }
    }

    Alert.alert('Add document', 'Choose source', [
      { text: 'Take Photo', onPress: () => void takePhoto() },
      { text: 'Choose File', onPress: () => void chooseFile() },
      { text: 'Cancel', style: 'cancel' },
    ])
  }

  const handleCompleteOrder = async () => {
    if (!order?.orderId) return
    try {
      setLoading(true)
      await updateOrderStatus(order.orderId as string, 3)
      Alert.alert('Success', 'Order marked as completed')
      const refreshed = await getOrder(order.orderId as string)
      setOrder(refreshed)
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to complete order')
      Alert.alert('Error', err instanceof Error ? err.message : 'Failed to complete order')
    } finally {
      setLoading(false)
    }
  }

  const totalLoads = (order?.loads ? order.loads.length : 0)
  const totalWeight = (order?.loads
    ? order.loads.reduce((sum: number, load: Load) => sum + (load.weight ?? 0), 0)
    : 0)

  if (loading) {
    return (
      <View className="bg-primary flex-1 items-center justify-center">
        <ActivityIndicator color="#fff" />
      </View>
    )
  }

  if (error) {
    return (
      <View className="bg-primary flex-1 items-center justify-center px-5">
        <Text className="text-red-400 text-sm">{error}</Text>
      </View>
    )
  }

  return (
    <View className="bg-primary flex-1">
      <ScrollView contentContainerStyle={{ paddingTop: 32, paddingBottom: 160, paddingHorizontal: 20 }}>
        <View className="mt-5">
          <Text className="text-white font-bold text-xl" numberOfLines={2}>
            Order nr.: {order?.orderId}
          </Text>
        </View>

        <View className="mt-4">
          <Text className="text-gray-400 text-sm">Pickup</Text>
          <Text className="text-white text-base font-semibold" numberOfLines={2}>
            {order?.pickupLocation}
          </Text>
        </View>

        <View className="mt-3">
          <Text className="text-gray-400 text-sm">Delivery</Text>
          <Text className="text-white text-base font-semibold" numberOfLines={2}>
            {order?.deliveryLocation}
          </Text>
        </View>

        <View className="mt-4 flex-row justify-between">
          <View>
            <Text className="text-gray-400 text-sm">Loads</Text>
            <Text className="text-white text-base font-semibold">{totalLoads}</Text>
          </View>
          <View className="items-end">
            <Text className="text-gray-400 text-sm">Total weight</Text>
            <Text className="text-white text-base font-semibold">{totalWeight}</Text>
          </View>
        </View>

        {order?.loads && order.loads.length > 0 && (
          <View className="mt-6">
            <Text className="text-white font-semibold text-base mb-2">Loads detail</Text>
            {order.loads.map((load: Load, index: number) => (
              <View key={index} className="mb-3 border border-gray-700 rounded-lg p-3">
                <Text className="text-gray-300 text-sm">Weight: {load.weight}</Text>
                {load.description ? (
                  <Text className="text-gray-300 text-sm mt-1" numberOfLines={3}>
                    {load.description}
                  </Text>
                ) : null}
              </View>
            ))}
          </View>
        )}
      </ScrollView>

      <View className="absolute bottom-12 w-full px-5">
        {order?.status === 0 ? (
          <TouchableOpacity
            className="bg-blue-600 rounded-xl py-3 items-center"
            onPress={handleApply}
          >
            <Text className="text-white font-semibold text-base">Apply</Text>
          </TouchableOpacity>
        ) : order?.status === 2 ? (
          <View className="flex-row justify-between space-x-3">
            <TouchableOpacity
              className="bg-gray-700 rounded-xl py-3 px-3 flex-1 items-center"
              onPress={handleAddNote}
            >
              <Text className="text-white font-semibold text-base">Add Note</Text>
            </TouchableOpacity>
            <TouchableOpacity
              className="bg-gray-700 rounded-xl py-3 px-3 flex-1 items-center"
              onPress={handleAddDocuments}
            >
              <Text className="text-white font-semibold text-base">Add Documents</Text>
            </TouchableOpacity>
            <TouchableOpacity
              className="bg-green-600 rounded-xl py-3 px-3 flex-1 items-center"
              onPress={handleCompleteOrder}
            >
              <Text className="text-white font-semibold text-base">Complete Order</Text>
            </TouchableOpacity>
          </View>
        ) : null}
      </View>
    </View>
  )
}

export default OrderDetails