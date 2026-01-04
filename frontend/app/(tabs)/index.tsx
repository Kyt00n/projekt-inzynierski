import React, { useMemo } from "react";
import SearchBar from "@/components/searchBar";
import { ActivityIndicator, FlatList, ScrollView, Text, View } from "react-native";
import { useRouter } from "expo-router";
import useFetch from "@/services/useFetch";
import { getUserOrders } from "@/services/api";
import PostCard from "@/components/postCard";
import { Order } from "@/interfaces/Order";
import { useAuth } from "../authProvider";
import OrderCard from "@/components/orderCard";
import TripCard from '@/components/tripCard'

export default function Index() {
  const router = useRouter();
  const {authState} = useAuth();
  const {
    data: orders,
    loading,
    error,
    refetch,
  } = useFetch<Order[]>(() => getUserOrders(authState?.userId as string));
  const sortedOrders = useMemo(() => {
    return (orders ?? []).slice().sort((a: Order, b: Order) => {
      const aStatus = typeof a.status === 'number' ? a.status : 0
      const bStatus = typeof b.status === 'number' ? b.status : 0
      if (aStatus !== bStatus) return aStatus - bStatus

      const aTrip = a.tripId ?? ''
      const bTrip = b.tripId ?? ''
      if (aTrip === bTrip) return 0
      if (aTrip === '') return 1
      if (bTrip === '') return -1

      const aNum = Number(aTrip)
      const bNum = Number(bTrip)
      if (!isNaN(aNum) && !isNaN(bNum)) return aNum - bNum
      return String(aTrip).localeCompare(String(bTrip))
    })
  }, [orders])
  
  type ListItem =
    | { type: 'single'; order: Order }
    | { type: 'group'; tripId: string; status: number; orders: Order[] }

  const grouped = useMemo(() => {
    const groups = new Map<string, Order[]>()
    const result: ListItem[] = []

    for (const o of sortedOrders) {
      const t = o.tripId ?? ''
      if (t) {
        if (!groups.has(t)) groups.set(t, [])
        groups.get(t)!.push(o)
      } else {
        result.push({ type: 'single', order: o })
      }
    }

    const seen = new Set<string>()
    for (const o of sortedOrders) {
      const t = o.tripId ?? ''
      if (!t) continue
      if (seen.has(t)) continue
      const arr = groups.get(t) ?? []
      const grpStatus = arr.reduce(
        (acc, cur) => Math.min(acc, typeof cur.status === 'number' ? cur.status : 0),
        Infinity,
      )
      result.push({ type: 'group', tripId: t, status: isFinite(grpStatus) ? grpStatus : 0, orders: arr })
      seen.add(t)
    }

    return result
  }, [sortedOrders])

  const renderItem = ({ item }: { item: ListItem }) => {
    if (item.type === 'single') {
      return (
        <View className="items-center mb-4">
          <OrderCard {...item.order} />
        </View>
      )
    }

    return (
      <TripCard tripId={item.tripId} status={item.status}>
        {item.orders.map((o) => (
          // render OrderCard directly, without extra wrapper so it aligns inside TripCard
          <OrderCard key={o.orderId} {...o} />
        ))}
      </TripCard>
    )
  }

  return (
    <View className="flex-1 bg-primary">
          <View className="flex-1 px-5" style={{ paddingTop: 60 }}>
            <Text className="w-12 h-10 mb-4 mx-auto text-center text-4xl">🚚</Text>
            <Text className="mx-auto text-center text-3xl text-white font-bold">Your Trips</Text>
    
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
                data={grouped}
                keyExtractor={(item) => (item.type === 'single' ? item.order.orderId : `group-${item.tripId}`)}
                renderItem={renderItem}
                contentContainerStyle={{ paddingBottom: 100, paddingTop: 20 }}
                showsVerticalScrollIndicator={true}
                refreshing={loading}
                onRefresh={refetch}
                ListEmptyComponent={() => (
                  <View className="mt-10 items-center px-4">
                    <Text className="text-gray-400 text-center">
                      You have no trips, go to active orders to browse through newest offers
                    </Text>
                    <Text
                      className="text-white mt-3 underline"
                      onPress={() => router.push('/activeOrders')}
                    >
                      Browse active orders
                    </Text>
                  </View>
                )}
              />
            )}
          </View>
        </View>
  );
}
