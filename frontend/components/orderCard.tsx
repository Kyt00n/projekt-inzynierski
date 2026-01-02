import { View, Text, TouchableOpacity, Image } from 'react-native'
import React from 'react'
import { Order } from '@/interfaces/Order'
import { Link } from 'expo-router'

const OrderCard = ({id, pickupLocation, deliveryLocation, totalLoads, totalWeight }: Order) => {
  return (
    <Link href={`./order/${id}`} asChild>
        <TouchableOpacity className="w-[90%] flex-row items-start">
        <View className="w-2/3 pr-3">
          <Text className="text-white text-sm font-semibold mt-2" numberOfLines={1}>
            Order nr.: {id}
          </Text>
          <Text className="text-gray-400 text-xs mt-1" numberOfLines={1}>
            Pickup: {pickupLocation}
          </Text>
          <Text className="text-gray-400 text-xs mt-1" numberOfLines={1}>
            Delivery: {deliveryLocation}
          </Text>
        </View>
        <View className="w-1/3 items-end">
          <Text className="text-white text-sm font-semibold mt-2" numberOfLines={1}>
            Loads: {totalLoads}
          </Text>
          <Text className="text-gray-400 text-xs mt-1" numberOfLines={1}>
            Weight: {totalWeight}
          </Text>
        </View>
      </TouchableOpacity>
    </Link>
  )
}

export default OrderCard