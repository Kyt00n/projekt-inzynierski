import React from 'react'
import { ActivityIndicator, FlatList, Text, View } from 'react-native'
import OrderCard from '@/components/orderCard'
import { getActiveOrders } from '@/services/api'
import useFetch from '@/services/useFetch'
import { Order } from '@/interfaces/Order'

export default function ActiveOrders() {
  const {
    data: orders,
    loading,
    error,
    refetch,
  } = useFetch<Order[]>(() => getActiveOrders())

  const renderItem = ({ item }: { item: Order }) => (
    <View className="items-center mb-4">
      <OrderCard {...item} />
    </View>
  )

  return (
    <View className="flex-1 bg-primary">
      <View className="flex-1 px-5" style={{ paddingTop: 60 }}>
        <Text className="w-12 h-10 mb-4 mx-auto text-center text-4xl">🚚</Text>
        <Text className="mx-auto text-center text-3xl text-white font-bold">Active Orders</Text>

        {loading ? (
          <ActivityIndicator size="large" color="#0000ff" className="mt-10 self-center" />
        ) : error ? (
          <View className="mt-6 items-center">
            <Text className="text-red-400 text-sm mb-2">{error.message}</Text>
            <Text className="text-white underline" onPress={refetch}>
              Retry
            </Text>
          </View>
        ) : (
          <FlatList
            data={orders ?? []}
            keyExtractor={(item) => item.orderId}
            renderItem={renderItem}
            contentContainerStyle={{ paddingBottom: 100, paddingTop: 20 }}
            showsVerticalScrollIndicator={false}
            refreshing={loading}
            onRefresh={refetch}
            ListEmptyComponent={
              <Text className="text-gray-400 text-center mt-10">No active orders</Text>
            }
          />
        )}
      </View>
    </View>
  )
}
