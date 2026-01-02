import { ActivityIndicator, ScrollView, Text, TouchableOpacity, View } from 'react-native'
import React, { useEffect, useState } from 'react'
import { useLocalSearchParams } from 'expo-router'
import { Image, ScrollView as HorizontalScrollView } from 'react-native'
import { Load } from '@/interfaces/Load'
import { getOrder } from '@/services/api'
import { Order } from '@/interfaces/Order'

const OrderDetails = () => {
  const { id } = useLocalSearchParams()
    
  const [order, setOrder] = useState<Order | null>(null)
  const [loading, setLoading] = useState(true)
  const [error, setError] = useState<string | null>(null)

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

  const totalLoads = order?.totalLoads ?? (order?.loads ? order.loads.length : 0)
  const totalWeight = order?.totalWeight ?? (order?.loads
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
      <ScrollView contentContainerStyle={{ paddingBottom: 120, paddingHorizontal: 20 }}>
        <View className="mt-5">
          <Text className="text-white font-bold text-xl" numberOfLines={2}>
            Order nr.: {order?.id}
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

      <View className="absolute bottom-5 w-full px-5">
        <TouchableOpacity
          className="bg-blue-600 rounded-xl py-3 items-center"
          onPress={() => {
            // TODO: hook up apply action
          }}
        >
          <Text className="text-white font-semibold text-base">Apply</Text>
        </TouchableOpacity>
      </View>
    </View>
  )
}

export default OrderDetails