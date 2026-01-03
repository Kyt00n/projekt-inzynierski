import { View, Text, TouchableOpacity, Image } from 'react-native'
import React from 'react'
import { Order } from '@/interfaces/Order'
import { Link } from 'expo-router'
import { Load } from '@/interfaces/Load'

const OrderCard = (props: Order) => {
  const {
    orderId,
    pickupLocation,
    deliveryLocation,
    status,
    loads = [],
  } = props

  const totalLoads = (loads ?? []).length
  const totalWeight = (loads ?? []).reduce((sum: number, load: Load) => sum + (Number(load.weight) || 0), 0)

  let borderClass = 'border-transparent'
  let badgeMessage: string | null = null
  let badgeBg = ''
  let badgeTextColor = 'text-white'
  let lineThrough = false

  switch (status) {
    case 1:
      borderClass = 'border-2 border-yellow-400'
      badgeMessage = 'waiting for accept'
      badgeBg = 'bg-yellow-400'
      badgeTextColor = 'text-black'
      break
    case 2:
      borderClass = 'border-2 border-green-500'
      badgeMessage = 'accepted'
      badgeBg = 'bg-green-500'
      break
    case 3:
      borderClass = 'border-2 border-blue-500'
      badgeMessage = 'Completed'
      badgeBg = 'bg-blue-500'
      lineThrough = true
      break
    case 4:
      borderClass = 'border-2 border-red-500'
      badgeMessage = 'Cancelled'
      badgeBg = 'bg-red-500'
      lineThrough = true
      break
    default:
      // Created or unknown -> no border/badge
      break
  }

  const textDecorationClass = lineThrough ? 'line-through text-gray-400' : ''

  return (
    <Link href={`/order/${orderId}`} asChild>
      <TouchableOpacity className={`w-[90%] relative flex-row items-start rounded-lg p-3 bg-primary-700 ${borderClass}`}>
        {badgeMessage ? (
          <View className={`absolute right-3 top-3 px-2 py-1 rounded-full ${badgeBg}`}>
            <Text className={`text-xs ${badgeTextColor}`}>{badgeMessage}</Text>
          </View>
        ) : null}

        <View className="w-2/3 pr-3">
          <Text className={`text-white text-sm font-semibold mt-2 ${textDecorationClass}`} numberOfLines={1}>
            Order nr.: {orderId}
          </Text>
          <Text className={`text-gray-400 text-xs mt-1 ${textDecorationClass}`} numberOfLines={1}>
            Pickup: {pickupLocation}
          </Text>
          <Text className={`text-gray-400 text-xs mt-1 ${textDecorationClass}`} numberOfLines={1}>
            Delivery: {deliveryLocation}
          </Text>
        </View>

        <View className="w-1/3 items-end">
          <Text className={`text-white text-sm font-semibold mt-2 ${textDecorationClass}`} numberOfLines={1}>
            Loads: {totalLoads}
          </Text>
          <Text className={`text-gray-400 text-xs mt-1 ${textDecorationClass}`} numberOfLines={1}>
            Weight: {totalWeight}
          </Text>
        </View>
      </TouchableOpacity>
    </Link>
  )
}

export default OrderCard